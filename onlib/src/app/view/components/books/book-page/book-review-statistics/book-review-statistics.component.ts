import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges } from '@angular/core';
import { ReviewService } from '../../../../../business/services/shelves/review.service';
import { ReviewStatistics } from '../../../../../business/models/shelves/reviewStatistics';
import { BookRatingComponent } from '../book-rating/book-rating.component';

@Component({
  standalone: true,
  selector: 'book-review-statistics',
  imports: [CommonModule, BookRatingComponent],
  templateUrl: './book-review-statistics.component.html',
  styleUrl: './book-review-statistics.component.scss',
})
export class BookReviewStatisticsComponent implements OnChanges {
  @Input({ required: true }) bookId!: string;

  public statistics: ReviewStatistics | undefined;

  constructor(private reviewService: ReviewService) {}

  public ngOnChanges(): void {
    this.reviewService.getStatistics(this.bookId).subscribe((x) => {
      this.statistics = x;
    });
  }
}
