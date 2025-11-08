import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable, switchMap, take } from 'rxjs';
import { AuthorPreview } from '../../models/books/apiModels/authorPreview';
import { AuthService } from '../auth/auth.service';
import { ApiResult } from '../../models/_shared/apiResult';
import { environment } from '../../../environments/environment';
import { CreateAuthorRequest } from '../../models/books/requests/createAuthorRequest';
import { Result } from '../../models/_shared/result';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { UpdateAuthorRequest } from '../../models/books/requests/updateAuthorRequest';
import { FullAuthor } from '../../models/books/fullAuthor';

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

  public delete(authorId: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/delete/${authorId}`;
    return this.connection
      .delete<ApiResult<void>>(url)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public update(request: UpdateAuthorRequest): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/update`;
    return this.connection
      .post<ApiResult<void>>(url, request)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public getFull(authorId: string): Observable<FullAuthor | undefined> {
    const url = `${this.CONTROLLER}/full/${authorId}`;
    return this.connection.get<FullAuthor | undefined>(url);
  }

  public isFullNameTaken(
    firstName: string,
    lastName: string,
    middleName?: string
  ): Observable<boolean> {
    let params = new HttpParams()
      .set('firstName', firstName)
      .set('lastName', lastName);
    
    if (middleName) {
      params = params.set('middleName', middleName);
    }
    
    const url = `${this.CONTROLLER}/full-name-taken`;
    return this.connection.get<boolean>(url, { params });
  }
}
