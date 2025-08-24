import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../header/navbar/navbar.component';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { CommonModule } from '@angular/common';
import { BookPreviewComponent } from '../book-preview/book-preview.component';

@Component({
  selector: 'books-page',
  imports: [NavbarComponent, CommonModule, BookPreviewComponent],
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
