import { JsonPipe } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { Button } from "primeng/button";

@Component({
  selector: 'book-creation-window',
  imports: [ReactiveFormsModule, JsonPipe, Button],
  templateUrl: './book-creation-window.component.html',
  styleUrl: './book-creation-window.component.scss',
})
export class BookCreationWindowComponent {
  public form: FormGroup;

  constructor(private ref: DynamicDialogRef, formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      title: ['', Validators.required],
    });
  }

  public submit(): void {
    console.log('submit');

    if (this.form.invalid) {
      return;
    }
  }
}
