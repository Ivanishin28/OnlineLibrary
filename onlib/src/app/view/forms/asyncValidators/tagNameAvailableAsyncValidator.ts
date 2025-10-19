import { AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { TagService } from '../../../business/services/shelves/tag.service';
import { map, of } from 'rxjs';
import { UserId } from '../../../business/models/_shared/userId';

export function tagNameAvailableAsyncValidator(
  tagService: TagService,
  userId: UserId
): AsyncValidatorFn {
  return (control: AbstractControl) => {
    if (!control.value) {
      return of(null);
    }

    return tagService
      .isTagNameTakenByUser(userId, control.value)
      .pipe(
        map((isTaken: boolean) => (isTaken ? { tagNameTaken: true } : null))
      );
  };
}
