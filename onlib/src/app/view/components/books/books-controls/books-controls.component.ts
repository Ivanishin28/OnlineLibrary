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

@Component({
  standalone: true,
  selector: 'books-controls',
  imports: [CommonModule, ButtonModule, DynamicDialogModule],
  providers: [BookCreationWindowManager, BookService],
  templateUrl: './books-controls.component.html',
  styleUrl: './books-controls.component.scss',
})
export class BooksControlsComponent implements OnInit {
  private user!: UserId;

  public books: BookPreview[] = [];

  constructor(
    private auth: AuthService,
    private bookService: BookService,
    private bookCreationWindow: BookCreationWindowManager
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
    this.bookService.getByUserId(this.user).subscribe((x) => {
      console.log(x);
      this.books = x;
    });
  }
}
