import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ApiResult } from '../../models/_shared/apiResult';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { RegisterRequest } from '../../models/identity/registerRequest';
import { LoginRequest } from '../../models/identity/loginRequest';
import { LoginResult } from '../../models/identity/loginResult';
import { UserId } from '../../models/_shared/userId';
import { IdentityPreview } from '../../models/identity/identityPreview';

@Injectable()
export class AccountService {
  private readonly COMPONENT = 'api/identity/ApplicationUser';

  constructor(private http: HttpClient) {}

  public register(request: RegisterRequest): Observable<Result<void>> {
    const url = `${environment.api_main}/${this.COMPONENT}/register`;

    return this.http.post<ApiResult<void>>(url, request).pipe(
      map((apiResult) => resultFromApiResult(apiResult)),
      catchError((error: HttpErrorResponse) => {
        const apiResult = error.error as ApiResult<void>;
        return of(resultFromApiResult(apiResult));
      })
    );
  }

  public login(request: LoginRequest): Observable<Result<LoginResult>> {
    const url = `${environment.api_main}/${this.COMPONENT}/login`;

    return this.http.post<ApiResult<LoginResult>>(url, request).pipe(
      map((apiResult) => resultFromApiResult(apiResult)),
      catchError((error: HttpErrorResponse) => {
        const apiResult = error.error as ApiResult<LoginResult>;
        return of(resultFromApiResult(apiResult));
      })
    );
  }

  public getIdentityBy(userId: UserId): Observable<IdentityPreview> {
    const url = `${environment.api_main}/${this.COMPONENT}/preview/${userId.value}`;

    return this.http.get<IdentityPreview>(url);
  }
}
