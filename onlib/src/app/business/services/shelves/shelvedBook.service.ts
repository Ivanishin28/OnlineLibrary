import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserId } from '../../models/_shared/userId';
import { map, Observable, switchMap, take } from 'rxjs';
import { ShelvedBook } from '../../models/shelves/shelvedBook';
import { environment } from '../../../environments/environment';
import { Result } from '../../models/_shared/result';
import { ApiResult } from '../../models/_shared/apiResult';
import { ShelveBookRequest } from '../../models/shelves/shelveBookRequest';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { AddTagToBookRequest } from '../../models/shelves/addTagToBookRequest';
import { AuthService } from '../auth/auth.service';
import { RemoveTagFromBookRequest } from '../../models/shelves/removeTagFromBookRequest';

@Injectable()
export class ShelvedBookService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/shelvedbook`;

  constructor(
    private authService: AuthService,
    private connection: HttpClient
  ) {}

  public get(bookId: string): Observable<ShelvedBook | undefined> {
    return this.authService.loggedUser$.pipe(
      take(1),
      switchMap((creds) => {
        const url = `${this.CONTROLLER}/user/${creds.userId.value}/book/${bookId}`;
        return this.connection.get<ShelvedBook | undefined>(url);
      })
    );
  }

  public shelve(shelfId: string, bookId: string): Observable<Result<string>> {
    const request: ShelveBookRequest = {
      book_id: bookId,
      shelf_id: shelfId,
    };

    const url = `${this.CONTROLLER}/shelve`;
    return this.connection
      .post<ApiResult<string>>(url, request)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public addTag(
    shelvedBookId: string,
    tagId: string
  ): Observable<Result<void>> {
    const request: AddTagToBookRequest = {
      shelved_book_id: shelvedBookId,
      tag_id: tagId,
    };

    const url = `${this.CONTROLLER}/add-tag`;
    return this.connection
      .post<ApiResult<void>>(url, request)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public removeTag(
    shelvedBookId: string,
    tagId: string
  ): Observable<Result<void>> {
    const request: RemoveTagFromBookRequest = {
      shelved_book_id: shelvedBookId,
      tag_id: tagId,
    };

    const url = `${this.CONTROLLER}/remove-tag`;
    return this.connection
      .post<ApiResult<void>>(url, request)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public dislodge(shelvedBookId: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/dislodge/${shelvedBookId}`;

    return this.connection
      .delete<ApiResult<void>>(url)
      .pipe(map((x) => resultFromApiResult(x)));
  }
}
