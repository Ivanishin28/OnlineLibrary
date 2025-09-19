import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AccountService } from './account.service';
import { StorageService } from '../_shared/storage.service';
import { LoginRequest } from '../../models/identity/loginRequest';
import { Result } from '../../models/_shared/result';
import { AuthStorageKeys } from '../../consts/authStorageKeys';
import { LoginResult } from '../../models/identity/loginResult';

@Injectable()
export class AuthService {
  private userId: BehaviorSubject<string | undefined> = new BehaviorSubject<
    string | undefined
  >(undefined);

  public readonly userId$: Observable<string | undefined> =
    this.userId.asObservable();

  constructor(
    private accountService: AccountService,
    private storageService: StorageService
  ) {}

  public login(loginRequest: LoginRequest): Observable<Result<LoginResult>> {
    return this.accountService.login(loginRequest).pipe(
      tap((result) => {
        if (!result.isSuccess) {
          return;
        }

        this.storageService.set(AuthStorageKeys.USER_ID, result.value.user_id);
        this.userId.next(result.value.user_id);
      })
    );
  }

  public logout(): void {
    this.userId.next(undefined);
  }
}
