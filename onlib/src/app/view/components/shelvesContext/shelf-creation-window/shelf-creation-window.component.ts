import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ShelfNameConsts } from '../../../../business/consts/shelfContext/shelfNameConsts';
import { InputTextModule } from 'primeng/inputtext';
import { markAllAsDirty } from '../../../../business/helpers/forms/markAllAsDirty';
import { ValidationSummaryComponent } from "../../_shared/validation-summary/validation-summary.component";

@Component({
  standalone: true,
  selector: 'shelf-creation-window',
  imports: [CommonModule, ReactiveFormsModule, InputTextModule, ValidationSummaryComponent],
  templateUrl: './shelf-creation-window.component.html',
  styleUrl: './shelf-creation-window.component.scss',
})
export class ShelfCreationWindowComponent implements OnInit {
  public form!: FormGroup<{
    name: FormControl<string | null>;
  }>;

  constructor(private builder: FormBuilder) {}

  public ngOnInit(): void {
    this.form = this.builder.group({
      name: [
        '',
        [Validators.required, Validators.maxLength(ShelfNameConsts.MAX_LENGTH)],
      ],
    });
  }

  public onSubmit(): void {
    markAllAsDirty(this.form);

    if (this.form.invalid) {
      return;
    }

    console.log(this.form.value);
  }
}
