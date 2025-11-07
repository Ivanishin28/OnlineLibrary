import { CommonModule, JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Button } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DatePickerModule } from 'primeng/datepicker';
import { BookCreationWindowOutput } from '../../../../business/models/books/bookCreationWindowOutput';
import { MediaFileUploadComponent } from '../../_shared/media-file-upload/media-file-upload.component';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';
import { AuthorSelectionComponent } from '../author-selection/author-selection.component';
import { GenreSelectionComponent } from '../genre-selection/genre-selection.component';
import { BookCoverComponent } from '../book-cover/book-cover.component';
import { AuthorPreview } from '../../../../business/models/books/apiModels/authorPreview';
import { Genre } from '../../../../business/models/shelves/genre';
import { FullBook } from '../../../../business/models/books/fullBook';
import { toDate } from '../../../../business/types/dateOnly';

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
    BookCoverComponent,
    AuthorSelectionComponent,
    GenreSelectionComponent,
  ],
  templateUrl: './book-creation-window.component.html',
  styleUrl: './book-creation-window.component.scss',
})
export class BookCreationWindowComponent implements OnInit {
  public form: FormGroup<{
    title: FormControl<string | null>;
    publishing_date: FormControl<Date | null>;
    description: FormControl<string | null>;
  }>;

  public coverId: string | undefined;
  public selectedAuthors: AuthorPreview[] = [];
  public selectedGenres: Genre[] = [];
  public isEditMode: boolean = false;

  constructor(
    private ref: DynamicDialogRef,
    private formBuilder: FormBuilder,
    private config: DynamicDialogConfig
  ) {
    const fullBook: FullBook | undefined = config.data?.fullBook;
    this.isEditMode = !!fullBook;

    this.form = formBuilder.group({
      title: ['', Validators.required],
      publishing_date: [null as Date | null, Validators.required],
      description: [''],
    });
  }

  public ngOnInit(): void {
    const fullBook: FullBook | undefined = this.config.data?.fullBook;
    if (fullBook) {
      this.loadBook(fullBook);
    }
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
      this.coverId ?? null,
      this.selectedGenres
    );

    this.ref.close(output);
  }

  public onCoverUploaded(fileId: MediaFileId): void {
    this.coverId = fileId.value;
  }

  public onSelectedAuthorsChange(authors: AuthorPreview[]): void {
    this.selectedAuthors = authors;
  }

  public onSelectedGenresChange(genres: Genre[]): void {
    this.selectedGenres = genres;
  }

  private loadBook(fullBook: FullBook): void {
    this.form.controls.title.setValue(fullBook.title);
    this.form.controls.publishing_date.setValue(
      toDate(fullBook.publishing_date)
    );
    this.form.controls.description.setValue(fullBook.description ?? '');
    if (fullBook.cover_id) {
      this.coverId = fullBook.cover_id;
    }
    this.selectedAuthors = fullBook.authors.map((author): AuthorPreview => ({
      id: author.id,
      first_name: author.first_name,
      last_name: author.last_name,
      birth_date: toDate(author.birth_date),
      avatar_id: author.avatar_id ?? '',
    }));
    this.selectedGenres = fullBook.genres ?? [];
  }
}
