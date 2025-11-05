import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { filter, map, Observable, switchMap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { ReviewerService } from '../../services/shelves/reviewer.service';
import { ReviewCreationWindowComponent } from '../../../view/components/shelvesContext/review-creation-window/review-creation-window.component';
import { ReviewCreationWindowOutput } from '../../models/shelves/reviewCreationWindowOutput';
import { AddBookReviewRequest } from '../../models/shelves/addBookReviewRequest';

@Injectable()
export class ReviewCreationWindowManager {
  constructor(
    private dialog: DialogService,
    private reviewService: ReviewerService
  ) {}

  public createReviewFor(
    bookId: string
  ): Observable<Result<ReviewCreationWindowOutput>> {
    const ref = this.dialog.open(ReviewCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Review Book',
      modal: true,
      data: { bookId },
    });

    return ref.onClose.pipe(
      filter((output: ReviewCreationWindowOutput | undefined) => !!output),
      switchMap((output: ReviewCreationWindowOutput) => {
        const request: AddBookReviewRequest = {
          book_id: bookId,
          rating: output.rating,
          text: output.text,
        };
        return this.reviewService
          .addReview(request)
          .pipe(
            map((x) =>
              x.isSuccess
                ? Result.success(output)
                : x.toFailure<ReviewCreationWindowOutput>()
            )
          );
      })
    );
  }
}
