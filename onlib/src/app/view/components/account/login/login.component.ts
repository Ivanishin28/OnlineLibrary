import { Component } from '@angular/core';
import { AuthService } from '../../../../business/services/auth/authService';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { Result } from '../../../../business/models/_shared/result';
import { ValidationSummaryComponent } from '../../_shared/validation-summary/validation-summary.component';
import { markAllAsDirty } from '../../../../business/helpers/forms/markAllAsDirty';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    InputTextModule,
    PasswordModule,
    RouterModule,
    ValidationSummaryComponent,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  public form: FormGroup<{
    login: FormControl<string | null>;
    password: FormControl<string | null>;
  }>;

  public error: Result<void> | undefined;

  constructor(
    private authService: AuthService,
    private router: Router,

    builder: FormBuilder
  ) {
    this.form = builder.group({
      login: new FormControl('', {
        validators: [Validators.required],
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
      .login({
        login: this.form.value.login!,
        password: this.form.value.password!,
      })
      .subscribe((loginResult) => {
        if (loginResult.isSuccess) {
          console.log('scuccess');
        } else {
          this.error = loginResult;
        }
      });
  }
}
