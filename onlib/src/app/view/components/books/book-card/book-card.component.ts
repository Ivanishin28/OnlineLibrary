import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { BookCoverComponent } from '../book-cover/book-cover.component';
import { BookPreview } from '../../../../business/models/books/bookPreview';

@Component({
  standalone: true,
  selector: 'book-card',
  imports: [CommonModule, BookCoverComponent],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.scss',
})
export class BookCardComponent {
  @Input({ required: true }) book!: BookPreview;
}
