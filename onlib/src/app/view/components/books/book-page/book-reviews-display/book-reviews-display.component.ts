import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { ReviewService } from '../../../../../business/services/shelves/review.service';
import { ReviewPreview } from '../../../../../business/models/shelves/reviewPreview';
import { Paginator } from '../../../../../business/models/_shared/paginator';

@Component({
  standalone: true,
  selector: 'book-reviews-display',
  imports: [CommonModule],
  templateUrl: './book-reviews-display.component.html',
  styleUrl: './book-reviews-display.component.scss',
})
export class BookReviewsDisplayComponent implements OnChanges, OnDestroy {
  @Input({ required: true }) bookId!: string;

  private destroy$: Subject<void> = new Subject<void>();

  public reviews: ReviewPreview[] | undefined;
  public paginator: Paginator;

  constructor(private reviewService: ReviewService) {
    this.paginator = new Paginator({ page_index: 0, page_size: 10 });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnChanges(): void {
    this.reviewService
      .getPage({
        book_id: this.bookId,
        page: this.paginator.page,
      })
      .subscribe((pagination) => {
        this.reviews = pagination.items;
      });
  }
}
