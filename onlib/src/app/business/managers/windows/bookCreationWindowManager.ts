import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { BookCreationWindowComponent } from '../../../view/components/booksPage/book-creation-window/book-creation-window.component';
import { filter, map, Observable } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { BookPreview } from '../../models/books/bookPreview';

@Injectable()
export class BookCreationWindowManager {
  constructor(private dialog: DialogService) {}

  public create(): Observable<Result<void>> {
    const ref = this.dialog.open(BookCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Create Book',
    });
    return ref.onClose.pipe(
      filter((output: BookPreview | undefined) => !!output),
      map((output: BookPreview) => {
        return Result.success<void>(void 0);
      })
    );
  }
}
