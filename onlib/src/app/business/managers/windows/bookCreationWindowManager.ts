import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { filter, map, Observable, switchMap, tap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { CreateBookRequest } from '../../models/books/createBookRequest';
import { BookService } from '../../services/books/book.service';
import { BookCreationWindowComponent } from '../../../view/components/books/book-creation-window/book-creation-window.component';
import { BookCreationWindowOutput } from '../../models/books/bookCreationWindowOutput';

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
        const author_ids = (output.author_ids_input || '')
          .split(',')
          .map((x) => x.trim())
          .filter((x) => !!x);

        const request: CreateBookRequest = {
          title: output.title,
          author_ids,
          publishing_date: output.publishing_date,
          cover_id: output.cover_id,
          description: output.description,
        };

        return this.bookService.create(request);
      })
    );
  }
}
