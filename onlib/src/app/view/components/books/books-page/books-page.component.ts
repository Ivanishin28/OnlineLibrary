import { Component, OnDestroy, OnInit } from '@angular/core';
import { BookService } from '../../../../business/services/books/book.service';
import { CommonModule } from '@angular/common';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { BookEvents } from '../../../../business/services/books/bookEvents';
import { BookFilterComponent } from './book-filter/book-filter.component';
import { BooksPageDisplayComponent } from './books-page-display/books-page-display.component';
import { BookFilter } from '../../../../business/models/shelves/bookFilter';
import { Paginator } from '../../../../business/models/_shared/paginator';
import { Subject, takeUntil } from 'rxjs';
import { Pagination } from '../../../../business/models/_shared/pagination';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { PaginationComponent } from '../../_shared/pagination/pagination.component';

@Component({
  standalone: true,
  selector: 'books-page',
  imports: [
    CommonModule,
    DynamicDialogModule,
    BookFilterComponent,
    BooksPageDisplayComponent,
    PaginationComponent,
  ],
  providers: [BookService, BookEvents],
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.scss',
})
export class BooksPageComponent implements OnInit, OnDestroy {
  private destory$ = new Subject<void>();

  public filter: BookFilter;
  public paginator: Paginator;
  public bookPage: Pagination<BookPreview> | undefined;

  constructor(private bookService: BookService) {
    this.filter = {
      genre_ids: [],
    };
    this.paginator = new Paginator({ page_index: 0, page_size: 10 });
  }

  public ngOnDestroy(): void {
    this.destory$.next();
    this.destory$.complete();
  }

  public ngOnInit(): void {
    this.paginator.paginationChanged$
      .pipe(takeUntil(this.destory$))
      .subscribe((page) => {
        this.bookService
          .getBookPage(this.filter, page)
          .subscribe((x) => (this.bookPage = x));
      });
    this.paginator.loadFirstPage();
  }

  public filterBooks(filter: BookFilter): void {
    this.filter = filter;
    this.paginator.loadFirstPage();
  }
}
