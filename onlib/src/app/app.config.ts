import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { LaraLightBlue } from '../assets/theme';
import { StorageService } from './business/services/_shared/storage.service';
import { AccountService } from './business/services/auth/account.service';
import { AuthService } from './business/services/auth/auth.service';
import { DialogService } from 'primeng/dynamicdialog';
import { tokenAppendingInterceptor } from './business/interceptors/token-appending.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptors([tokenAppendingInterceptor])),
    providePrimeNG({
      theme: {
        preset: LaraLightBlue,
        options: {
          darkModeSelector: 'none',
          primitive: 'red',
        },
      },
    }),

    StorageService,
    AccountService,
    AuthService,
    DialogService,
  ],
};
