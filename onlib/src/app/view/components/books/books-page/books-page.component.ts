import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { CommonModule } from '@angular/common';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { RouterLink } from '@angular/router';
import { BookCardComponent } from "../book-card/book-card.component";

@Component({
  standalone: true,
  selector: 'books-page',
  imports: [
    CommonModule,
    DynamicDialogModule,
    BookCardComponent
],
  providers: [BookService],
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
