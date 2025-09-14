import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ApiResult } from '../../models/_shared/apiResult';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { RegisterRequest } from '../../models/identity/registerRequest';

@Injectable()
export class AuthService {
  private readonly COMPONENT = 'api/identity/ApplicationUser';

  constructor(private http: HttpClient) {}

  public register(request: RegisterRequest): Observable<Result<void>> {
    const url = `${environment.api_main}/${this.COMPONENT}/register`;

    return this.http
      .post<ApiResult<void>>(url, request)
      .pipe(map((apiResult) => resultFromApiResult(apiResult)));
  }
}
