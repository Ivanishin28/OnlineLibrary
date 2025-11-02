import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { BookService } from '../../../../business/services/books/book.service';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { BookSearchResultComponent } from './book-search-result/book-search-result.component';
import {
  Subject,
  debounceTime,
  distinctUntilChanged,
  switchMap,
  catchError,
  of,
  takeUntil,
  Observable,
  filter,
} from 'rxjs';
import { BookQueryConsts } from '../../../../business/consts/bookContext/bookQueryConsts';

@Component({
  standalone: true,
  selector: 'book-search',
  imports: [FormsModule, AutoCompleteModule, BookSearchResultComponent],
  templateUrl: './book-search.component.html',
  styleUrl: './book-search.component.scss',
  providers: [BookService],
})
export class BookSearchComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();
  private searchSubject: Subject<string> = new Subject<string>();

  public selectedBook: BookPreview | undefined = undefined;
  public filteredBooks: BookPreview[] = [];

  constructor(private bookService: BookService) {}

  public ngOnInit(): void {
    this.searchSubject
      .pipe(
        filter(
          (x) => !!x && x.length >= BookQueryConsts.MIN_BOOK_SEARCH_QUERY_LENGTH
        ),
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((query: string): Observable<BookPreview[]> => {
          if (!query || query.trim().length === 0) {
            return of([] as BookPreview[]);
          }
          return this.bookService
            .search(query.trim())
            .pipe(catchError(() => of([] as BookPreview[])));
        }),
        takeUntil(this.destroy$)
      )
      .subscribe((books: BookPreview[]) => {
        this.filteredBooks = books;
      });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public searchBooks(event: { query: string }): void {
    this.searchSubject.next(event.query);
  }
}
