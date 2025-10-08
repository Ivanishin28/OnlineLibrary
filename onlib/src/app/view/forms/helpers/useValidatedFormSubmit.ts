import { Observable, tap, switchMap, startWith, filter, take } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { markAllAsDirty } from './markAllAsDirty';

export function useValidatedFormSubmit<T>(form: FormGroup) {
  return (source$: Observable<T>) =>
    source$.pipe(
      tap(() => markAllAsDirty(form)),
      switchMap(() =>
        form.statusChanges.pipe(
          startWith(form.status),
          filter((status) => status !== 'PENDING'),
          take(1)
        )
      ),
      filter((status) => status === 'VALID')
    );
}
