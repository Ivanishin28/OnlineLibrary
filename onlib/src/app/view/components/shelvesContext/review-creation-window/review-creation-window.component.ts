import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ReviewCreationWindowOutput } from '../../../../business/models/shelves/reviewCreationWindowOutput';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { Subject, takeUntil } from 'rxjs';
import { useValidatedFormSubmit } from '../../../forms/helpers/useValidatedFormSubmit';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';

@Component({
  selector: 'app-review-creation-window',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    ButtonModule,
    RatingModule,
  ],
  templateUrl: './review-creation-window.component.html',
  styleUrl: './review-creation-window.component.scss',
})
export class ReviewCreationWindowComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public formSubmit$: Subject<void> = new Subject<void>();

  public bookId: string;
  public form!: FormGroup<{
    rating: FormControl<number | null>;
    text: FormControl<string | null>;
  }>;

  constructor(
    private ref: DynamicDialogRef,
    private builder: FormBuilder,
    config: DynamicDialogConfig
  ) {
    this.bookId = config.data.bookId;
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    this.form = this.builder.group({
      rating: new FormControl<number | null>(0, {
        validators: [Validators.required, Validators.min(1), Validators.max(5)],
        updateOn: 'submit',
      }),
      text: new FormControl<string | null>('', {
        validators: [Validators.required, Validators.maxLength(500)],
        updateOn: 'submit',
      }),
    });

    this.formSubmit$
      .pipe(takeUntil(this.destroy$), useValidatedFormSubmit(this.form))
      .subscribe(() => this.onSubmit());
  }

  private onSubmit(): void {
    const output = new ReviewCreationWindowOutput(
      this.form.value.rating!,
      this.form.value.text!
    );
    this.ref.close(output);
  }
}
