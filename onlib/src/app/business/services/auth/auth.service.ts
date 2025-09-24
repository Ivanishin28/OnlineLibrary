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
import { Token } from '../../models/_shared/token';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  private user: BehaviorSubject<UserCredentials | undefined> =
    new BehaviorSubject<UserCredentials | undefined>(undefined);
  private token: BehaviorSubject<Token | undefined> = new BehaviorSubject<
    Token | undefined
  >(undefined);

  public readonly user$: Observable<UserCredentials | undefined> =
    this.user.asObservable();
  public readonly loggedUser$: Observable<UserCredentials> = this.user.pipe(
    map((x) => x!)
  );
  public readonly token$: Observable<Token | undefined> =
    this.token.asObservable();

  constructor(
    private accountService: AccountService,
    private storageService: StorageService,
    private router: Router
  ) {}

  public init(credentials: UserCredentials, token: Token): void {
    this.user.next(credentials);
    this.token.next(token);
  }

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
        this.user.next(model);

        const token = new Token(result.value.token.value);
        this.storageService.set(AuthStorageKeys.TOKEN, token);
        this.token.next(token);
      })
    );
  }

  public logout(): void {
    this.user.next(undefined);
    this.token.next(undefined);
    this.storageService.clear();
    this.router.navigate(['/account/login']);
  }
}
