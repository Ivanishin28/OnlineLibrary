import { Component, OnInit } from '@angular/core';
import { UserId } from '../../../../business/models/_shared/userId';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { Observable, take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { BookService } from '../../../../business/services/books/book.service';
import { BookCreationWindowManager } from '../../../../business/managers/windows/bookCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { PersonalBooksService } from '../../../../business/services/books/personal-books.service';
import { BookCoverComponent } from "../book-cover/book-cover.component";
import { BookCardComponent } from "../book-card/book-card.component";

@Component({
  standalone: true,
  selector: 'books-controls',
  imports: [CommonModule, ButtonModule, DynamicDialogModule, BookCoverComponent, BookCardComponent],
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
    this.bookCreationWindow.create().subscribe((x) => console.log(x));
  }

  private loadBooks(): void {
    this.personalBooksService.getPersonalBooks().subscribe((x) => {
      this.books = x;
    });
  }
}
