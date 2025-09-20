import { Injectable } from '@angular/core';
import { BehaviorSubject, filter, map, Observable, tap } from 'rxjs';
import { AccountService } from './account.service';
import { StorageService } from '../_shared/storage.service';
import { LoginRequest } from '../../models/identity/loginRequest';
import { Result } from '../../models/_shared/result';
import { AuthStorageKeys } from '../../consts/authStorageKeys';
import { LoginResult } from '../../models/identity/loginResult';
import { UserCredentials } from '../../models/_shared/userCredentials';
import { UserId } from '../../models/_shared/userId';
import { IdentityId } from '../../models/_shared/identityId';

@Injectable()
export class AuthService {
  private userId: BehaviorSubject<UserCredentials | undefined> =
    new BehaviorSubject<UserCredentials | undefined>(undefined);

  public readonly userId$: Observable<UserCredentials | undefined> =
    this.userId.asObservable();
  public readonly loggedUserId$: Observable<UserCredentials> = this.userId.pipe(
    map((x) => x!)
  );

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

        const model = new UserCredentials(
          new IdentityId(result.value.identity_id),
          new UserId(result.value.user_id)
        );
        this.storageService.set(AuthStorageKeys.USER_CREDENTIALS, model);

        this.userId.next(model);
      })
    );
  }

  public set(credentials: UserCredentials): void {
    this.userId.next(credentials);
  }

  public logout(): void {
    this.userId.next(undefined);
  }
}
