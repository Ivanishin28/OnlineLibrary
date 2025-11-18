import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { Subject, combineLatest, takeUntil, switchMap } from 'rxjs';
import { ShelfService } from '../../../../../business/services/shelves/shelf.service';
import { ShelfForBook } from '../../../../../business/models/shelves/shelfForBook';
import { BookEvents } from '../../../../../business/services/books/bookEvents';

@Component({
  standalone: true,
  selector: 'book-shelved-statistics',
  imports: [CommonModule],
  providers: [ShelfService],
  templateUrl: './book-shelved-statistics.component.html',
  styleUrl: './book-shelved-statistics.component.scss',
})
export class BookShelvedStatisticsComponent
  implements OnInit, OnChanges, OnDestroy
{
  @Input({ required: true }) bookId!: string;

  private destroy$: Subject<void> = new Subject<void>();

  public shelvedCount: number | undefined;
  public shelves: ShelfForBook[] = [];

  public showOverlay = false;
  public overlayShelves: ShelfForBook[] = [];
  public overlayLoading = false;

  constructor(
    private shelfService: ShelfService,
    private bookEvents: BookEvents
  ) {}

  public ngOnInit(): void {
    this.loadStatistics();

    this.bookEvents.bookShelved$
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.shelvedCount = (this.shelvedCount ?? 0) + 1;
      });

    this.bookEvents.bookDislodged$
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => (this.shelvedCount = (this.shelvedCount ?? 1) - 1));
  }

  public ngOnChanges(): void {
    this.loadStatistics();
  }

  private loadStatistics(): void {
    combineLatest([
      this.shelfService.getShelvedCount(this.bookId),
      this.shelfService.getAllShelfsForBook(this.bookId),
    ]).subscribe(([count, shelves]) => {
      this.shelvedCount = count;
      this.shelves = shelves;
    });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public onShelvedCountClick(): void {
    if (this.showOverlay) {
      this.showOverlay = false;
      return;
    }

    this.overlayLoading = true;
    this.shelfService
      .getAllShelfsForBook(this.bookId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (shelves) => {
          this.overlayShelves = shelves;
          this.overlayLoading = false;
          this.showOverlay = true;
        },
        error: () => {
          this.overlayLoading = false;
        },
      });
  }

  public closeOverlay(): void {
    this.showOverlay = false;
  }
}
