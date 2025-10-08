import { FormControl, FormGroup } from '@angular/forms';

export const markAllAsDirty = (form: FormGroup): void => {
  Object.values(form.controls).forEach((control) => {
    if (control instanceof FormControl) {
      control.markAsDirty();
    } else if (control instanceof FormGroup) {
      markAllAsDirty(control);
    }
  });
};
