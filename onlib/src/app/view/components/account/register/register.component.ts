import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { Result } from '../../../../business/models/_shared/result';
import { markAllAsDirty } from '../../../../business/helpers/forms/markAllAsDirty';
import { Router, RouterModule } from '@angular/router';
import { ValidationSummaryComponent } from '../../_shared/validation-summary/validation-summary.component';
import { DatePickerModule } from 'primeng/datepicker';

@Component({
  standalone: true,
  selector: 'app-register',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    RouterModule,
    ValidationSummaryComponent,
    DatePickerModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent implements OnInit {
  public form!: FormGroup<{
    login: FormControl<string | null>;
    email: FormControl<string | null>;
    password: FormControl<string | null>;
    firstName: FormControl<string | null>;
    lastName: FormControl<string | null>;
    birthDate: FormControl<Date | null>;
  }>;

  public error: Result<void> | undefined;

  constructor(
    private authService: AuthService,
    private router: Router,
    private builder: FormBuilder
  ) {}

  public ngOnInit(): void {
    this.form = this.builder.group({
      login: new FormControl('', {
        validators: [Validators.required],
      }),
      email: new FormControl('', {
        validators: [Validators.email, Validators.required],
      }),
      password: new FormControl('', {
        validators: [Validators.required],
      }),
      birthDate: new FormControl<Date | null>(null, {
        validators: [Validators.required],
      }),
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
    });
  }

  public onSubmit(): void {
    markAllAsDirty(this.form);

    if (this.form.invalid) {
      return;
    }

    this.authService
      .register({
        login: this.form.value.login!,
        email: this.form.value.email!,
        password: this.form.value.password!,
        first_name: this.form.value.firstName!,
        last_name: this.form.value.lastName!,
        birth_date: this.form.value.birthDate!,
      })
      .subscribe((result) => {
        console.log(result);
        if (result.isSuccess) {
          this.navigateToLogin();
        } else {
          this.showError(result);
        }
      });
  }

  public showError(failure: Result<void>): void {
    this.error = failure;
  }

  public navigateToLogin(): void {
    this.router.navigate(['account/login']);
  }
}
