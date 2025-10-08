import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { TagCreationWindowOutput } from '../../../../business/models/shelves/tagCreationWindowOutput';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { tagNameAvailableAsyncValidator } from '../../../forms/asyncValidators/tagNameAvailableAsyncValidator';
import { Subject, takeUntil } from 'rxjs';
import { useValidatedFormSubmit } from '../../../forms/helpers/useValidatedFormSubmit';
import { TagService } from '../../../../business/services/shelves/tag.service';
import { UserId } from '../../../../business/models/_shared/userId';

@Component({
  selector: 'tag-creation-window',
  imports: [CommonModule, ReactiveFormsModule, InputTextModule],
  templateUrl: './tag-creation-window.component.html',
  styleUrl: './tag-creation-window.component.scss',
})
export class TagCreationWindowComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public formSubmit$: Subject<void> = new Subject<void>();

  public userId: UserId;
  public form!: FormGroup<{
    name: FormControl<string | null>;
  }>;

  constructor(
    private ref: DynamicDialogRef,
    private tagService: TagService,
    private builder: FormBuilder,

    config: DynamicDialogConfig
  ) {
    this.userId = config.data;
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    this.form = this.builder.group({
      name: new FormControl<string | null>('', {
        validators: [Validators.required],
        asyncValidators: [
          tagNameAvailableAsyncValidator(this.tagService, this.userId),
        ],
        updateOn: 'submit',
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

    const output = new TagCreationWindowOutput(this.form.value.name!);
    this.ref.close(output);
  }
}
