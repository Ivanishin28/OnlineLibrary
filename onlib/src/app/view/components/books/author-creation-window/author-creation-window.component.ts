import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { AuthorCreationWindowOutput } from '../../../../business/models/books/authorCreationWindowOutput';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { Subject, takeUntil } from 'rxjs';
import { useValidatedFormSubmit } from '../../../forms/helpers/useValidatedFormSubmit';
import { ButtonModule } from 'primeng/button';
import { MediaFileUploadComponent } from "../../_shared/media-file-upload/media-file-upload.component";

@Component({
  selector: 'author-creation-window',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    DatePickerModule,
    ButtonModule,
    MediaFileUploadComponent
],
  templateUrl: './author-creation-window.component.html',
  styleUrl: './author-creation-window.component.scss',
})
export class AuthorCreationWindowComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public formSubmit$: Subject<void> = new Subject<void>();

  public form!: FormGroup<{
    first_name: FormControl<string | null>;
    last_name: FormControl<string | null>;
    birth_date: FormControl<Date | null>;
  }>;

  constructor(private ref: DynamicDialogRef, private builder: FormBuilder) {}

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    this.form = this.builder.group({
      first_name: new FormControl<string | null>('', {
        validators: [Validators.required],
      }),
      last_name: new FormControl<string | null>('', {
        validators: [Validators.required],
      }),
      birth_date: new FormControl<Date | null>(null, {
        validators: [Validators.required],
      }),
    });

    this.formSubmit$
      .pipe(takeUntil(this.destroy$), useValidatedFormSubmit(this.form))
      .subscribe(() => this.onSubmit());
  }

  private onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const output = new AuthorCreationWindowOutput(
      this.form.value.first_name!,
      this.form.value.last_name!,
      this.form.value.birth_date!
    );
    this.ref.close(output);
  }
}
