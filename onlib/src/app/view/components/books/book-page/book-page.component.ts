import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { BookService } from '../../../../business/services/books/book.service';
import { FullBook } from '../../../../business/models/books/fullBook';
import { BookPageActionsComponent } from './book-page-actions/book-page-actions.component';
import { BooksPageComponent } from '../books-page/books-page.component';
import { BookCoverComponent } from '../book-cover/book-cover.component';
import { Button } from 'primeng/button';
import { ReviewCreationWindowManager } from '../../../../business/managers/windows/reviewCreationWindowManager';
import { BookReviewsDisplayComponent } from './book-reviews-display/book-reviews-display.component';
import { BookReviewStatisticsComponent } from "./book-review-statistics/book-review-statistics.component";

@Component({
  standalone: true,
  selector: 'book-page',
  imports: [
    CommonModule,
    RouterModule,
    BookPageActionsComponent,
    BooksPageComponent,
    BookCoverComponent,
    Button,
    BookReviewsDisplayComponent,
    BookReviewStatisticsComponent
],
  providers: [BookService, ReviewCreationWindowManager],
  templateUrl: './book-page.component.html',
  styleUrl: './book-page.component.scss',
})
export class BookPageComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public book: FullBook | undefined;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService,
    private reviewWindow: ReviewCreationWindowManager
  ) {}

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(
        takeUntil(this.destroy$),
        switchMap((params) => {
          const bookId = params.get('id')!;
          window.scrollTo(0, 0);
          return this.bookService.getFull(bookId);
        })
      )
      .subscribe((book) => {
        this.book = book;
      });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
  }

  public review(): void {
    if (!this.book) {
      return;
    }

    this.reviewWindow.createReviewFor(this.book?.id).subscribe();
  }
}
