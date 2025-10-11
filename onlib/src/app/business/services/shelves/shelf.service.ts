import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { ShelfPreview as ShelfPreview } from '../../models/shelves/shelfPreview';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Result } from '../../models/_shared/result';
import { CreateShelfRequest } from '../../models/shelves/createShelfRequest';
import { CreateShelfResponse } from '../../models/shelves/createShelfResponse';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { ApiResult } from '../../models/_shared/apiResult';
import { UserId } from '../../models/_shared/userId';

@Injectable()
export class ShelfService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/shelf`;

  constructor(private connection: HttpClient) {}

  public getByUserId(userId: UserId): Observable<ShelfPreview[]> {
    const url = `${this.CONTROLLER}/user/${userId.value}`;

    return this.connection.get<ShelfPreview[]>(url);
  }

  public create(
    requst: CreateShelfRequest
  ): Observable<Result<CreateShelfResponse>> {
    const url = `${environment.api_main}/create`;

    return this.connection
      .post<ApiResult<CreateShelfResponse>>(url, requst)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public delete(shelfId: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/delete/${shelfId}`;
    return this.connection
      .delete<ApiResult<void>>(url)
      .pipe(map((x) => resultFromApiResult(x)));
  }
}
