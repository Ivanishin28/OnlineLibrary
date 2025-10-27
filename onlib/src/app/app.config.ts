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
import { TagService } from './business/services/shelves/tag.service';
import { BookService } from './business/services/books/book.service';
import { ShelfService } from './business/services/shelves/shelf.service';
import { ReviewService } from './business/services/shelves/review.service';
import { ReviewCreationWindowManager } from './business/managers/windows/reviewCreationWindowManager';
import { OnlibDatePipe } from './view/pipes/onlib-date.pipe';
import { DatePipe } from '@angular/common';
import { MediaFileService } from './business/services/media/media-file.service';

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
    TagService,
    BookService,
    ShelfService,
    ReviewService,
    ReviewCreationWindowManager,
    MediaFileService,
    DatePipe,
  ],
};
