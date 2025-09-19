import { Injectable } from '@angular/core';
import { AuthService } from './business/services/auth/auth.service';
import { EMPTY, Observable, of } from 'rxjs';
import { StorageService } from './business/services/_shared/storage.service';
import { AuthStorageKeys } from './business/consts/authStorageKeys';

@Injectable()
export class Application {
  constructor(
    private storageManager: StorageService,
    private authService: AuthService
  ) {}

  public start(): Observable<void> {
    const userId = this.storageManager.get(AuthStorageKeys.USER_ID);

    if (userId) {
      this.authService.setUserId(userId);
    }

    return of(void 0);
  }
}
