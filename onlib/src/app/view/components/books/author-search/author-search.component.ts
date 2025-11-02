import {
  Component,
  OnInit,
  OnDestroy,
  Output,
  EventEmitter,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { AuthorService } from '../../../../business/services/books/author.service';
import { AuthorPreview } from '../../../../business/models/books/apiModels/authorPreview';
import { AuthorSearchResultComponent } from './author-search-result/author-search-result.component';
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
  selector: 'author-search',
  imports: [FormsModule, AutoCompleteModule, AuthorSearchResultComponent],
  templateUrl: './author-search.component.html',
  styleUrl: './author-search.component.scss',
  providers: [AuthorService],
})
export class AuthorSearchComponent implements OnInit, OnDestroy {
  @Output() authorSelected: EventEmitter<AuthorPreview> =
    new EventEmitter<AuthorPreview>();

  private destroy$: Subject<void> = new Subject<void>();
  private searchSubject: Subject<string> = new Subject<string>();

  public filteredAuthors: AuthorPreview[] = [];

  constructor(private authorService: AuthorService) {}

  public ngOnInit(): void {
    this.searchSubject
      .pipe(
        filter(
          (x) => !!x && x.length >= BookQueryConsts.MIN_BOOK_SEARCH_QUERY_LENGTH
        ),
        debounceTime(300),
        distinctUntilChanged(),
        switchMap((query: string): Observable<AuthorPreview[]> => {
          if (!query || query.trim().length === 0) {
            return of([] as AuthorPreview[]);
          }
          return this.authorService
            .search(query.trim())
            .pipe(catchError(() => of([] as AuthorPreview[])));
        }),
        takeUntil(this.destroy$)
      )
      .subscribe((authors: AuthorPreview[]) => {
        this.filteredAuthors = authors;
      });
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public searchAuthors(event: { query: string }): void {
    this.searchSubject.next(event.query);
  }

  public selectAuthor(author: AuthorPreview): void {
    this.authorSelected.emit(author);
  }
}
