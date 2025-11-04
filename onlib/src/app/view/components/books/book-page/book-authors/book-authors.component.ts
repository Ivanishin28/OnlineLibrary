import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FullAuthor } from '../../../../../business/models/books/fullAuthor';

@Component({
  standalone: true,
  selector: 'book-authors',
  imports: [CommonModule],
  templateUrl: './book-authors.component.html',
  styleUrl: './book-authors.component.scss',
})
export class BookAuthorsComponent {
  @Input({ required: true }) authors!: FullAuthor[];
}

