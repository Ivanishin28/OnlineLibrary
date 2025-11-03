import { Injectable } from '@angular/core';
import { map, Observable, switchMap, take } from 'rxjs';
import { ShelfPreview as ShelfPreview } from '../../models/shelves/shelfPreview';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Result } from '../../models/_shared/result';
import { CreateShelfRequest } from '../../models/shelves/createShelfRequest';
import { CreateShelfResponse } from '../../models/shelves/createShelfResponse';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { ApiResult } from '../../models/_shared/apiResult';
import { UserId } from '../../models/_shared/userId';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class ShelfService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/shelf`;

  constructor(
    private connection: HttpClient,
    private authService: AuthService
  ) {}

  public getByUserId(userId: UserId): Observable<ShelfPreview[]> {
    const url = `${this.CONTROLLER}/user/${userId.value}`;

    return this.connection.get<ShelfPreview[]>(url);
  }

  public create(
    requst: CreateShelfRequest
  ): Observable<Result<CreateShelfResponse>> {
    const url = `${this.CONTROLLER}/create`;

    return this.connection
      .post<ApiResult<CreateShelfResponse>>(url, requst)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public rename(shelf_id: string, name: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/rename`;

    return this.connection
      .post<ApiResult<void>>(url, {
        shelf_id,
        name,
      })
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public isNameTaken(name: string): Observable<boolean> {
    return this.authService.loggedUser$.pipe(
      take(1),
      switchMap((creds) => {
        const url = `${this.CONTROLLER}/taken`;
        const request = { user_id: creds.userId.value, name: name };
        return this.connection.post<boolean>(url, request);
      })
    );
  }

  public delete(shelfId: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/delete/${shelfId}`;
    return this.connection
      .delete<ApiResult<void>>(url)
      .pipe(map((x) => resultFromApiResult(x)));
  }
}
