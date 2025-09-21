import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserId } from '../../models/_shared/userId';
import { map, Observable } from 'rxjs';
import { ShelvedBook } from '../../models/shelves/shelvedBook';
import { environment } from '../../../environments/environment';
import { Result } from '../../models/_shared/result';
import { ApiResult } from '../../models/_shared/apiResult';
import { ShelveBookRequest } from '../../models/shelves/shelveBookRequest';
import { resultFromApiResult } from '../mappings/fromApiResult';

@Injectable()
export class ShelvedBookService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/shelvedbook`;

  constructor(private connection: HttpClient) {}

  public get(
    userId: UserId,
    bookId: string
  ): Observable<ShelvedBook | undefined> {
    const url = `${this.CONTROLLER}/user/${userId.value}/book/${bookId}`;
    return this.connection.get<ShelvedBook | undefined>(url);
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
}
