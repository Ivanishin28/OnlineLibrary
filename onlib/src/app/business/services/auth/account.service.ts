import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ApiResult } from '../../models/_shared/apiResult';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { RegisterRequest } from '../../models/identity/registerRequest';
import { LoginRequest } from '../../models/identity/loginRequest';
import { LoginResult } from '../../models/identity/loginResult';

@Injectable()
export class AccountService {
  private readonly COMPONENT = 'api/identity/ApplicationUser';

  constructor(private http: HttpClient) {}

  public register(request: RegisterRequest): Observable<Result<void>> {
    const url = `${environment.api_main}/${this.COMPONENT}/register`;

    return this.http
      .post<ApiResult<void>>(url, request)
      .pipe(map((apiResult) => resultFromApiResult(apiResult)));
  }

  public login(request: LoginRequest): Observable<Result<LoginResult>> {
    const url = `${environment.api_main}/${this.COMPONENT}/login`;

    return this.http
      .post<ApiResult<LoginResult>>(url, request)
      .pipe(map((apiResult) => resultFromApiResult(apiResult)));
  }
}
