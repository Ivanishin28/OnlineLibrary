import { Component, OnInit } from '@angular/core';
import { UserId } from '../../../../business/models/_shared/userId';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { BookCreationWindowManager } from '../../../../business/managers/windows/bookCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { PersonalBooksService } from '../../../../business/services/books/personal-books.service';
import { BookCoverComponent } from '../book-cover/book-cover.component';

@Component({
  standalone: true,
  selector: 'books-controls',
  imports: [
    CommonModule,
    ButtonModule,
    DynamicDialogModule,
    BookCoverComponent,
  ],
  providers: [BookCreationWindowManager, PersonalBooksService],
  templateUrl: './books-controls.component.html',
  styleUrl: './books-controls.component.scss',
})
export class BooksControlsComponent implements OnInit {
  private user!: UserId;

  public books: BookPreview[] = [];

  constructor(
    private auth: AuthService,
    private bookCreationWindow: BookCreationWindowManager,
    private personalBooksService: PersonalBooksService
  ) {}

  public ngOnInit(): void {
    this.auth.loggedUser$
      .pipe(take(1))
      .subscribe((x) => (this.user = x.userId));

    this.loadBooks();
  }

  public createBook(): void {
    this.bookCreationWindow.create().subscribe((x) => this.loadBooks());
  }

  public editBook(bookId: string): void {
    this.bookCreationWindow.edit(bookId).subscribe((x) => this.loadBooks());
  }

  public delete(bookId: string): void {
    this.personalBooksService.delete(bookId).subscribe((result) => {
      if (result.isSuccess) {
        this.loadBooks();
      }
    });
  }

  private loadBooks(): void {
    this.personalBooksService.getPersonalBooks().subscribe((x) => {
      this.books = x;
    });
  }
}
