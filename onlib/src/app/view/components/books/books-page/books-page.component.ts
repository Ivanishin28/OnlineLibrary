import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { CommonModule } from '@angular/common';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { BookCardWithPopoverComponent } from '../book-card-with-popover/book-card-with-popover.component';
import { BookEvents } from '../../../../business/services/books/bookEvents';

@Component({
  standalone: true,
  selector: 'books-page',
  imports: [CommonModule, DynamicDialogModule, BookCardWithPopoverComponent],
  providers: [BookService, BookEvents],
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.scss',
})
export class BooksPageComponent implements OnInit {
  public books: BookPreview[] = [];

  constructor(private bookService: BookService) {}

  public ngOnInit(): void {
    this.bookService.getAll().subscribe((result) => {
      this.books = result;
    });
  }
}
