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
import { BookCreation } from '../../../../business/models/books/bookCreation';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  standalone: true,
  selector: 'book-creation-window',
  imports: [CommonModule, ReactiveFormsModule, Button, InputTextModule],
  templateUrl: './book-creation-window.component.html',
  styleUrl: './book-creation-window.component.scss',
})
export class BookCreationWindowComponent {
  public form: FormGroup<{ title: FormControl<string | null> }>;

  constructor(private ref: DynamicDialogRef, formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      title: ['', Validators.required],
    });
  }

  public submit(): void {
    if (this.form.invalid) {
      return;
    }

    const book = new BookCreation(this.form.value.title!);

    this.ref.close(book);
  }
}
