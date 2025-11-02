import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookPreview } from '../../../../../business/models/books/bookPreview';
import { BookCoverComponent } from '../../book-cover/book-cover.component';

@Component({
  standalone: true,
  selector: 'book-search-result',
  imports: [CommonModule, BookCoverComponent],
  templateUrl: './book-search-result.component.html',
  styleUrl: './book-search-result.component.scss',
})
export class BookSearchResultComponent {
  @Input({ required: true }) book!: BookPreview;
}
