import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AuthorCreationWindowOutput } from '../../../../business/models/books/authorCreationWindowOutput';
import { AuthorCreationWindowInput } from '../../../../business/models/books/authorCreationWindowInput';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { Subject, takeUntil } from 'rxjs';
import { useValidatedFormSubmit } from '../../../forms/helpers/useValidatedFormSubmit';
import { ButtonModule } from 'primeng/button';
import { MediaFileUploadComponent } from '../../_shared/media-file-upload/media-file-upload.component';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';
import { UserAvatarComponent } from '../../user/user-avatar/user-avatar.component';
import { toDate } from '../../../../business/types/dateOnly';
import { TextareaModule } from 'primeng/textarea';

@Component({
  selector: 'author-creation-window',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    DatePickerModule,
    ButtonModule,
    MediaFileUploadComponent,
    UserAvatarComponent,
    TextareaModule
],
  templateUrl: './author-creation-window.component.html',
  styleUrl: './author-creation-window.component.scss',
})
export class AuthorCreationWindowComponent implements OnInit, OnDestroy {
  private destroy$: Subject<void> = new Subject<void>();

  public formSubmit$: Subject<void> = new Subject<void>();

  public avatarId: string | undefined;
  public isEditMode: boolean = false;
  public authorId: string | undefined;

  public form!: FormGroup<{
    first_name: FormControl<string | null>;
    last_name: FormControl<string | null>;
    birth_date: FormControl<Date | null>;
    biography: FormControl<string | null>;
  }>;

  constructor(
    private ref: DynamicDialogRef,
    private builder: FormBuilder,
    private config: DynamicDialogConfig
  ) {
    const input: AuthorCreationWindowInput | undefined = this.config.data?.input;
    this.isEditMode = !!input;
  }

  public ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  public ngOnInit(): void {
    const input: AuthorCreationWindowInput | undefined = this.config.data?.input;
    const author = input?.author;

    this.form = this.builder.group({
      first_name: new FormControl<string | null>(
        { value: author?.first_name ?? '', disabled: this.isEditMode },
        {
          validators: [Validators.required],
        }
      ),
      last_name: new FormControl<string | null>(
        { value: author?.last_name ?? '', disabled: this.isEditMode },
        {
          validators: [Validators.required],
        }
      ),
      birth_date: new FormControl<Date | null>(
        author?.birth_date ? toDate(author.birth_date) : null,
        {
          validators: [Validators.required],
        }
      ),
      biography: new FormControl<string | null>(author?.biography ?? null),
    });

    if (author?.avatar_id) {
      this.avatarId = author.avatar_id;
    }

    if (author?.id) {
      this.authorId = author.id;
    }

    this.formSubmit$
      .pipe(takeUntil(this.destroy$), useValidatedFormSubmit(this.form))
      .subscribe(() => this.onSubmit());
  }

  private onSubmit(): void {
    if (this.form.invalid) {
      return;
    }

    const output = new AuthorCreationWindowOutput(
      this.form.getRawValue().first_name!,
      this.form.getRawValue().last_name!,
      this.form.value.birth_date!,
      this.authorId,
      this.avatarId,
      this.form.value.biography ?? undefined
    );
    this.ref.close(output);
  }

  public onAvatarUploaded(avatar: MediaFileId): void {
    this.avatarId = avatar.value;
  }
}
