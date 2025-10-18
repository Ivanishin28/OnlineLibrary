import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Subject, switchMap, take, takeUntil, tap } from 'rxjs';
import { LibraryFilter } from '../../../../business/models/shelves/libraryFilter';
import { LibrarySummary } from '../../../../business/models/shelves/librarySummary';
import { ShelvedBookService } from '../../../../business/services/shelves/shelvedBook.service';
import { UserId } from '../../../../business/models/_shared/userId';
import { LibraryService } from '../../../../business/services/shelves/library.service';
import { LibraryFilterComponent } from './library-filter/library-filter.component';
import { Paginator } from '../../../../business/models/_shared/paginator';
import { PaginationComponent } from '../../_shared/pagination/pagination.component';
import { Pagination } from '../../../../business/models/_shared/pagination';
import { LibraryShelvedBook } from '../../../../business/models/shelves/libraryShelvedBook';
import { LibraryBookDisplayComponent } from "./library-book-display/library-book-display.component";

@Component({
  standalone: true,
  selector: 'library',
  imports: [
    CommonModule,
    RouterModule,
    LibraryFilterComponent,
    PaginationComponent,
    LibraryBookDisplayComponent
],
  providers: [LibraryService],
  templateUrl: './library.component.html',
  styleUrl: './library.component.scss',
})
export class LibraryComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();
  private userId: UserId | undefined;
  private filter: LibraryFilter = { shelf_id: undefined, tag_id: undefined };

  public paginator: Paginator;
  public bookPage: Pagination<LibraryShelvedBook>;
  public librarySummary: LibrarySummary | undefined;

  constructor(
    private route: ActivatedRoute,
    private libraryService: LibraryService
  ) {
    this.bookPage = new Pagination<LibraryShelvedBook>(0, []);
    this.paginator = new Paginator({ page_index: 0, page_size: 10 });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    this.paginator.paginationChanged$
      .pipe(takeUntil(this.destroy$))
      .subscribe((x) => {
        this.libraryService
          .getLibraryPage({
            user_id: this.userId!.value,
            filter: this.filter,
            page: x,
          })
          .subscribe((page) => (this.bookPage = page));
      });

    this.route.paramMap
      .pipe(
        take(1),
        tap((x) => (this.userId = new UserId(x.get('userId')!))),
        switchMap(() => this.libraryService.getLibrarySummaryBy(this.userId!))
      )
      .subscribe((x) => {
        this.librarySummary = x;
      });

    this.paginator.loadFirstPage();
  }

  public setFilter(filter: LibraryFilter): void {
    this.filter = filter;
    this.paginator.loadFirstPage();
  }
}
