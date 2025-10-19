import { Component, Input } from '@angular/core';
import { BookPreview } from '../../../../business/models/books/bookPreview';

@Component({
  selector: 'book-preview',
  imports: [],
  templateUrl: './book-preview.component.html',
  styleUrl: './book-preview.component.scss',
})
export class BookPreviewComponent {
  @Input({ required: true }) book!: BookPreview;
}
