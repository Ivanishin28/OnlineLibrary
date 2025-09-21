import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { CommonModule } from '@angular/common';
import { BookPreviewComponent } from '../book-preview/book-preview.component';
import { BookCreationWindowManager } from '../../../../business/managers/windows/bookCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { RouterLink } from '@angular/router';

@Component({
  standalone: true,
  selector: 'books-page',
  imports: [
    CommonModule,
    BookPreviewComponent,
    DynamicDialogModule,
    RouterLink,
  ],
  providers: [BookService, BookCreationWindowManager],
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.scss',
})
export class BooksPageComponent implements OnInit {
  public books: BookPreview[] = [];

  constructor(
    private bookCreationWindowManager: BookCreationWindowManager,
    private bookService: BookService
  ) {}

  public ngOnInit(): void {
    this.bookService.getAll().subscribe((result) => {
      this.books = result;
    });
  }

  public createBook(): void {
    this.bookCreationWindowManager.create().subscribe((result) => {
    });
  }
}
