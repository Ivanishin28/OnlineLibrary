import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, switchMap, take } from 'rxjs';
import { AuthorPreview } from '../../models/books/apiModels/authorPreview';
import { AuthService } from '../auth/auth.service';
import { ApiResult } from '../../models/_shared/apiResult';
import { environment } from '../../../environments/environment';
import { CreateAuthorRequest } from '../../models/books/requests/createAuthorRequest';
import { Result } from '../../models/_shared/result';
import { resultFromApiResult } from '../mappings/fromApiResult';

@Injectable()
export class PersonalAuthorsService {
  private CONTROLLER = `${environment.api_main}/api/book/author`;

  constructor(
    private connection: HttpClient,
    private authService: AuthService
  ) {}

  public getPersonalAuthors(): Observable<AuthorPreview[]> {
    return this.authService.loggedUser$.pipe(
      take(1),
      switchMap((x) => {
        const url = `${this.CONTROLLER}/userId/${x.userId.value}`;
        return this.connection.get<AuthorPreview[]>(url);
      })
    );
  }

  public createAuthor(
    request: CreateAuthorRequest
  ): Observable<Result<string>> {
    const url = `${this.CONTROLLER}/create`;
    return this.connection
      .post<ApiResult<string>>(url, request)
      .pipe(map((x) => resultFromApiResult(x)));
  }
}
