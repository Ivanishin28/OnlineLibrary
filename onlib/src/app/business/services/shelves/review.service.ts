import { Injectable } from '@angular/core';
import { GetBookReviewsPageRequest } from '../../models/shelves/getBookReviewsPageRequest';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class ReviewService {
  private readonly COMPONENT = `${environment.api_main}/api/composition/review`;

  constructor(private connection: HttpClient) {}

  public getPage(request: GetBookReviewsPageRequest): Observable<any> {
    const url = `${this.COMPONENT}/reviews`;
    return this.connection.post(url, request);
  }
}
