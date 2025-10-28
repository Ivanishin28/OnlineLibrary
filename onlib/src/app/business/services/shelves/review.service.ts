import { Injectable } from '@angular/core';
import { GetBookReviewsPageRequest } from '../../models/shelves/getBookReviewsPageRequest';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../models/_shared/pagination';
import { ReviewPreview } from '../../models/shelves/reviewPreview';

@Injectable()
export class ReviewService {
  private readonly COMPONENT = `${environment.api_main}/api/composition/review`;

  constructor(private connection: HttpClient) {}

  public getPage(
    request: GetBookReviewsPageRequest
  ): Observable<Pagination<ReviewPreview>> {
    const url = `${this.COMPONENT}/reviews`;
    return this.connection.post<Pagination<ReviewPreview>>(url, request);
  }
}
