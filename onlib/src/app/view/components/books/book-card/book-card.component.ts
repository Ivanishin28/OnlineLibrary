import { CommonModule } from '@angular/common';
import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  Output,
  TemplateRef,
} from '@angular/core';
import { BookCoverComponent } from '../book-cover/book-cover.component';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'book-card',
  imports: [CommonModule, BookCoverComponent, RouterModule],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.scss',
})
export class BookCardComponent {
  @Input({ required: true }) book!: BookPreview;
  @Input() navigateOnImageClick: boolean = false;

  @ContentChild('bottom') bottomTemplate?: TemplateRef<any>;
}
