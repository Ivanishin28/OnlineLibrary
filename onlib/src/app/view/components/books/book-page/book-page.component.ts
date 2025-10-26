import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Subject, switchMap, takeUntil } from 'rxjs';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { BookPreviewComponent } from '../book-preview/book-preview.component';
import { BookPageActionsComponent } from './book-page-actions/book-page-actions.component';
import { BooksPageComponent } from '../books-page/books-page.component';
import { BookCoverComponent } from '../book-cover/book-cover.component';

@Component({
  standalone: true,
  selector: 'book-page',
  imports: [
    CommonModule,
    RouterModule,
    BookPageActionsComponent,
    BooksPageComponent,
    BookCoverComponent,
  ],
  providers: [BookService],
  templateUrl: './book-page.component.html',
  styleUrl: './book-page.component.scss',
})
export class BookPageComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public book: BookPreview | undefined;

  constructor(
    private route: ActivatedRoute,
    private bookService: BookService
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
}
