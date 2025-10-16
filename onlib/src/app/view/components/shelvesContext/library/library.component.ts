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

@Component({
  standalone: true,
  selector: 'library',
  imports: [
    CommonModule,
    RouterModule,
    LibraryFilterComponent,
    PaginationComponent,
  ],
  providers: [LibraryService],
  templateUrl: './library.component.html',
  styleUrl: './library.component.scss',
})
export class LibraryComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();
  private userId: UserId | undefined;

  public librarySummary: LibrarySummary | undefined;
  public paginator: Paginator;

  constructor(
    private route: ActivatedRoute,
    private libraryService: LibraryService
  ) {
    this.paginator = new Paginator({ pageIndex: 0, pageSize: 10 });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    this.paginator.paginationChanged$
      .pipe(takeUntil(this.destroy$))
      .subscribe((x) => {});

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
}
