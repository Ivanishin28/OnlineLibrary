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
export class AuthorSearchComponent {
  @Output() authorSelected: EventEmitter<AuthorPreview> =
    new EventEmitter<AuthorPreview>();

  public MIN_QUERY_LENGTH = BookQueryConsts.MIN_BOOK_SEARCH_QUERY_LENGTH;

  public filteredAuthors: AuthorPreview[] = [];

  constructor(private authorService: AuthorService) {}

  public searchAuthors(event: { query: string }): void {
    this.authorService
      .search(event.query.trim())
      .pipe(catchError(() => of([] as AuthorPreview[])))
      .subscribe((authors: AuthorPreview[]) => {
        this.filteredAuthors = authors;
      });
  }

  public selectAuthor(author: AuthorPreview): void {
    this.authorSelected.emit(author);
  }
}
