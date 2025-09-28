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
    const user = this.storageManager.get(AuthStorageKeys.USER_CREDENTIALS);
    const token = this.storageManager.get(AuthStorageKeys.TOKEN);

    if (user && token) {
      this.authService.init(user, token);
    }

    return of(void 0);
  }
}
