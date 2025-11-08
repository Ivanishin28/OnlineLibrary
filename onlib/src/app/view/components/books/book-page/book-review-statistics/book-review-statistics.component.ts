import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { ReviewService } from '../../../../../business/services/shelves/review.service';
import { ReviewStatistics } from '../../../../../business/models/shelves/reviewStatistics';
import { BookRatingComponent } from '../book-rating/book-rating.component';
import { combineLatest, Subject, takeUntil } from 'rxjs';
import { BookEvents } from '../../../../../business/services/books/bookEvents';

@Component({
  standalone: true,
  selector: 'book-review-statistics',
  imports: [CommonModule, BookRatingComponent],
  templateUrl: './book-review-statistics.component.html',
  styleUrl: './book-review-statistics.component.scss',
})
export class BookReviewStatisticsComponent
  implements OnInit, OnChanges, OnDestroy
{
  @Input({ required: true }) bookId!: string;

  private destory$: Subject<void> = new Subject<void>();

  public statistics: ReviewStatistics | undefined;

  constructor(
    private reviewService: ReviewService,
    private bookEvents: BookEvents
  ) {}

  public ngOnInit(): void {
    this.bookEvents.bookDislodged$
      .pipe(takeUntil(this.destory$))
      .subscribe(() => this.loadStatistics());

    this.bookEvents.bookReviewed$
      .pipe(takeUntil(this.destory$))
      .subscribe((rating) => {
        if (!this.statistics) {
          return;
        }

        this.statistics.avg_rating =
          (this.statistics.avg_rating * this.statistics.review_count + rating) /
          (this.statistics.review_count + 1);
        this.statistics.review_count++;
      });
  }

  public ngOnChanges(): void {
    this.loadStatistics();
  }

  public ngOnDestroy(): void {
    this.destory$.next();
    this.destory$.complete();
  }

  private loadStatistics(): void {
    this.reviewService.getStatistics(this.bookId).subscribe((x) => {
      this.statistics = x;
    });
  }
}
