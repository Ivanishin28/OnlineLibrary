import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { BookCardComponent } from '../book-card/book-card.component';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'book-card-with-actions',
  imports: [CommonModule, BookCardComponent, ButtonModule],
  templateUrl: './book-card-with-actions.component.html',
  styleUrl: './book-card-with-actions.component.scss',
})
export class BookCardWithActionsComponent {
  @Input({ required: true }) book!: BookPreview;
}
