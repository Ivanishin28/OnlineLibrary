import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnInit, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { ReviewService } from '../../../../../business/services/shelves/review.service';
import { ReviewPreview } from '../../../../../business/models/shelves/reviewPreview';
import { Paginator } from '../../../../../business/models/_shared/paginator';
import { Pagination } from '../../../../../business/models/_shared/pagination';
import { PaginationComponent } from '../../../_shared/pagination/pagination.component';
import { UserAvatarComponent } from '../../../user/user-avatar/user-avatar.component';
import { BookRatingComponent } from '../book-rating/book-rating.component';

@Component({
  standalone: true,
  selector: 'book-reviews-display',
  imports: [CommonModule, PaginationComponent, UserAvatarComponent, BookRatingComponent],
  templateUrl: './book-reviews-display.component.html',
  styleUrl: './book-reviews-display.component.scss',
})
export class BookReviewsDisplayComponent
  implements OnInit, OnChanges, OnDestroy
{
  @Input({ required: true }) bookId!: string;

  private destroy$: Subject<void> = new Subject<void>();

  public reviewPage: Pagination<ReviewPreview> | undefined;
  public paginator: Paginator;

  constructor(private reviewService: ReviewService) {
    this.paginator = new Paginator({ page_index: 0, page_size: 10 });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    this.paginator.paginationChanged$
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.loadReviews();
      });

    this.loadReviews();
  }

  public ngOnChanges(): void {
    this.paginator.loadFirstPage();
  }

  private loadReviews(): void {
    this.reviewService
      .getPage({
        book_id: this.bookId,
        page: this.paginator.page,
      })
      .subscribe((pagination) => {
        this.reviewPage = pagination;
      });
  }

  public onPaginationChange(page: {
    page_index: number;
    page_size: number;
  }): void {
    this.paginator.loadPagination(page);
  }
}
