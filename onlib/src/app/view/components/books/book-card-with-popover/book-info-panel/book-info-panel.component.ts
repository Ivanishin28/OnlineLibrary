import { Component, Input, OnChanges } from '@angular/core';
import { BookService } from '../../../../../business/services/books/book.service';
import { ReviewService } from '../../../../../business/services/shelves/review.service';
import { FullBook } from '../../../../../business/models/books/fullBook';
import { CommonModule } from '@angular/common';
import { BookReviewStatisticsComponent } from '../../book-page/book-review-statistics/book-review-statistics.component';
import { GenreDisplayComponent } from '../../genre-display/genre-display.component';

@Component({
  selector: 'book-info-panel',
  standalone: true,
  imports: [CommonModule, BookReviewStatisticsComponent, GenreDisplayComponent],
  templateUrl: './book-info-panel.component.html',
  styleUrl: './book-info-panel.component.scss',
})
export class BookInfoPanelComponent implements OnChanges {
  @Input({ required: true }) bookId!: string;

  public book: FullBook | undefined;

  constructor(private bookService: BookService) {}

  public ngOnChanges(): void {
    this.book = undefined;
    if (!!this.bookId) {
      this.bookService.getFull(this.bookId).subscribe((x) => (this.book = x));
    }
  }
}
