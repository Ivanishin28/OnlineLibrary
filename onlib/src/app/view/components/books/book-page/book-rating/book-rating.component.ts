import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'book-rating',
  imports: [CommonModule, RatingModule, FormsModule],
  templateUrl: './book-rating.component.html',
  styleUrl: './book-rating.component.scss',
})
export class BookRatingComponent {
  @Input({ required: true }) rating!: number;
  @Input() fontSize: number = 20;
}

