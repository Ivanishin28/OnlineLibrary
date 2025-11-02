import { CommonModule, JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { Button } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { BookCreationWindowOutput } from '../../../../business/models/books/bookCreationWindowOutput';
import { MediaFileUploadComponent } from '../../_shared/media-file-upload/media-file-upload.component';
import { MediaImageComponent } from '../../_shared/media-image/media-image.component';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';
import { AuthorSelectionComponent } from '../author-selection/author-selection.component';
import { AuthorPreview } from '../../../../business/models/books/apiModels/authorPreview';

@Component({
  standalone: true,
  selector: 'book-creation-window',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    Button,
    InputTextModule,
    DatePickerModule,
    MediaFileUploadComponent,
    MediaImageComponent,
    AuthorSelectionComponent,
  ],
  templateUrl: './book-creation-window.component.html',
  styleUrl: './book-creation-window.component.scss',
})
export class BookCreationWindowComponent {
  public form: FormGroup<{
    title: FormControl<string | null>;
    publishing_date: FormControl<Date | null>;
    description: FormControl<string | null>;
  }>;

  public cover: MediaFileId | undefined;
  public selectedAuthors: AuthorPreview[] = [];

  constructor(private ref: DynamicDialogRef, formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      title: ['', Validators.required],
      publishing_date: [null as Date | null, Validators.required],
      description: [''],
    });
  }

  public submit(): void {
    if (this.form.invalid) {
      return;
    }

    const output = new BookCreationWindowOutput(
      this.form.value.title!,
      this.form.value.publishing_date!,
      this.selectedAuthors,
      this.form.value.description ?? null,
      this.cover?.value ?? null
    );

    this.ref.close(output);
  }

  public onCoverUploaded(fileId: MediaFileId): void {
    this.cover = fileId;
  }

  public onSelectedAuthorsChange(authors: AuthorPreview[]): void {
    this.selectedAuthors = authors;
  }
}
