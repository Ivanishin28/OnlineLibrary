import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { filter, map, Observable, switchMap, tap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { CreateBookRequest } from '../../models/books/createBookRequest';
import { UpdateBookRequest } from '../../models/books/updateBookRequest';
import { BookService } from '../../services/books/book.service';
import { BookCreationWindowComponent } from '../../../view/components/books/book-creation-window/book-creation-window.component';
import { BookCreationWindowOutput } from '../../models/books/bookCreationWindowOutput';
import { FullBook } from '../../models/books/fullBook';
import { BusinessError } from '../../models/_shared/businessError';

@Injectable()
export class BookCreationWindowManager {
  constructor(
    private dialog: DialogService,
    private bookService: BookService
  ) {}

  public create(): Observable<Result<void>> {
    const ref = this.dialog.open(BookCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Create Book',
      modal: true,
    });

    return ref.onClose.pipe(
      filter((output: BookCreationWindowOutput | undefined) => !!output),
      switchMap((output: BookCreationWindowOutput) => {
        const author_ids = output.selectedAuthors.map((author) => author.id);

        const request: CreateBookRequest = {
          title: output.title,
          author_ids,
          publishing_date: output.publishing_date,
          cover_id: output.cover_id,
          description: output.description,
          genre_ids: output.selectedGenres.map((x) => x.id),
        };

        return this.bookService.create(request);
      })
    );
  }

  public edit(bookId: string): Observable<Result<void>> {
    return this.bookService.getFull(bookId).pipe(
      switchMap((fullBook: FullBook) => {
        if (!fullBook) {
          Result.failure([new BusinessError('Not found', 'Not found')]);
        }

        const ref = this.dialog.open(BookCreationWindowComponent, {
          closable: true,
          showHeader: true,
          header: 'Edit Book',
          modal: true,
          data: {
            fullBook: fullBook,
          },
        });

        return ref.onClose.pipe(
          filter((output: BookCreationWindowOutput | undefined) => !!output),
          switchMap((output: BookCreationWindowOutput) => {
            const author_ids = output.selectedAuthors.map(
              (author) => author.id
            );

            const request: UpdateBookRequest = {
              id: bookId,
              author_ids,
              publishing_date: output.publishing_date,
              cover_id: output.cover_id,
              description: output.description,
              genre_ids: output.selectedGenres.map((x) => x.id),
            };

            return this.bookService.update(request);
          })
        );
      })
    );
  }
}
