import { Injectable } from '@angular/core';
import { UserId } from '../../models/_shared/userId';
import { map, Observable, switchMap, take } from 'rxjs';
import { Tag } from '../../models/shelves/tag';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Result } from '../../models/_shared/result';
import { ApiResult } from '../../models/_shared/apiResult';
import { resultFromApiResult } from '../mappings/fromApiResult';
import { CreateTagRequest } from '../../models/shelves/createTagRequest';
import { AuthService } from '../auth/auth.service';

@Injectable()
export class TagService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/tag`;

  constructor(
    private authService: AuthService,
    private connection: HttpClient
  ) {}

  public getPersonalTags(): Observable<Tag[]> {
    return this.authService.loggedUser$.pipe(
      take(1),
      switchMap((creads) => {
        return this.getAllByUserId(creads.userId);
      })
    );
  }

  public getAllByUserId(userId: UserId): Observable<Tag[]> {
    const url = `${this.CONTROLLER}/user/${userId.value}`;
    return this.connection.get<Tag[]>(url);
  }

  public isTagNameTakenByUser(
    userId: UserId,
    name: string
  ): Observable<boolean> {
    const url = `${this.CONTROLLER}/name-taken?name=${name}&userId=${userId.value}`;
    return this.connection.get<boolean>(url);
  }

  public create(request: CreateTagRequest): Observable<Result<string>> {
    const url = `${this.CONTROLLER}/create`;
    return this.connection
      .post<ApiResult<string>>(url, request)
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public rename(tag_id: string, name: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/rename/${tag_id}?name=${name}`;

    return this.connection
      .post<ApiResult<void>>(url, {})
      .pipe(map((x) => resultFromApiResult(x)));
  }

  public delete(tagId: string): Observable<Result<void>> {
    const url = `${this.CONTROLLER}/delete/${tagId}`;
    return this.connection
      .delete<ApiResult<void>>(url)
      .pipe(map((x) => resultFromApiResult(x)));
  }
}
