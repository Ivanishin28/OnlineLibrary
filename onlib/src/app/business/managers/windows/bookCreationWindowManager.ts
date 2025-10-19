import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { BookCreationWindowComponent } from '../../../view/components/books/book-creation-window/book-creation-window.component';
import { filter, map, Observable, switchMap, tap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { CreateBookRequest } from '../../models/books/createBookRequest';
import { BookService } from '../../services/books/book.service';
import { BookCreation } from '../../models/books/bookCreation';

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
    });
    
    return ref.onClose.pipe(
      filter((output: BookCreation | undefined) => !!output),
      switchMap((output: BookCreation) => {
        const request = new CreateBookRequest(output.title);
        return this.bookService.create(request);
      })
    );
  }
}
