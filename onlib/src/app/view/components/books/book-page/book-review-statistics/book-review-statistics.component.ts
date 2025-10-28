import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges } from '@angular/core';
import { ReviewService } from '../../../../../business/services/shelves/review.service';
import { ReviewStatistics } from '../../../../../business/models/shelves/reviewStatistics';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'book-review-statistics',
  imports: [CommonModule, RatingModule, FormsModule],
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
