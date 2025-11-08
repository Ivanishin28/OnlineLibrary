import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookPreview } from '../../../../../business/models/books/bookPreview';
import { BookCardComponent } from '../../book-card/book-card.component';

@Component({
  selector: 'books-page-display',
  standalone: true,
  imports: [CommonModule, BookCardComponent],
  templateUrl: './books-page-display.component.html',
  styleUrl: './books-page-display.component.scss',
})
export class BooksPageDisplayComponent {
  @Input({ required: true }) books!: BookPreview[];
}
