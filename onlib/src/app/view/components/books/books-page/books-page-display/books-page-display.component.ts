import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookPreview } from '../../../../../business/models/books/bookPreview';
import { BookCardWithPopoverComponent } from '../../book-card-with-popover/book-card-with-popover.component';

@Component({
  selector: 'books-page-display',
  standalone: true,
  imports: [CommonModule, BookCardWithPopoverComponent],
  templateUrl: './books-page-display.component.html',
  styleUrl: './books-page-display.component.scss',
})
export class BooksPageDisplayComponent {
  @Input({ required: true }) books!: BookPreview[];
}
