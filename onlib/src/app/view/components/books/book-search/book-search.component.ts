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
export class BookSearchComponent {
  public MIN_QUERY_LENGTH = BookQueryConsts.MIN_BOOK_SEARCH_QUERY_LENGTH;

  public selectedBook: BookPreview | undefined = undefined;
  public filteredBooks: BookPreview[] = [];

  constructor(private bookService: BookService) {}

  public searchBooks(event: { query: string }): void {
    this.bookService
      .search(event.query.trim())
      .pipe(catchError(() => of([] as BookPreview[])))
      .subscribe((books: BookPreview[]) => {
        this.filteredBooks = books;
      });
  }
}
