import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
import { AuthService } from '../../../../business/services/auth/authService';
import { Result } from '../../../../business/models/_shared/result';
import { markAllAsDirty } from '../../../../business/helpers/forms/markAllAsDirty';
import { Router, RouterModule } from '@angular/router';

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
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  public form: FormGroup<{
    login: FormControl<string | null>;
    email: FormControl<string | null>;
    password: FormControl<string | null>;
  }>;

  public errorMessage: string | undefined;

  constructor(
    private authService: AuthService,
    private router: Router,

    builder: FormBuilder
  ) {
    this.form = builder.group({
      login: new FormControl('', {
        validators: [Validators.required],
      }),
      email: new FormControl('', {
        validators: [Validators.email, Validators.required],
      }),
      password: new FormControl('', {
        validators: [Validators.required],
      }),
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
      })
      .subscribe((result) => {
        if (result.isSuccess) {
          this.showError(result);
        } else {
          this.navigateToLogin();
        }
      });
  }

  public showError(failure: Result<void>): void {
    this.errorMessage = failure.errorMessage;
  }

  public navigateToLogin(): void {
    this.router.navigate(['account/login']);
  }
}
