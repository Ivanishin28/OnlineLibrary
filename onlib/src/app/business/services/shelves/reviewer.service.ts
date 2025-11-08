import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AddBookReviewRequest } from '../../models/shelves/addBookReviewRequest';
import { ApiResult } from '../../models/_shared/apiResult';
import { Result } from '../../models/_shared/result';
import { resultFromApiResult } from '../mappings/fromApiResult';

@Injectable()
export class ReviewerService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/review`;

  constructor(private http: HttpClient) {}

  public addReview(request: AddBookReviewRequest): Observable<Result<string>> {
    const url = `${this.CONTROLLER}`;

    return this.http
      .post<ApiResult<string>>(url, request)
      .pipe(map((apiResult) => resultFromApiResult(apiResult)));
  }
}
