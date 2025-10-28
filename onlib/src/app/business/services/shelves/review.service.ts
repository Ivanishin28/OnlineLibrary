import { Injectable } from '@angular/core';
import { GetBookReviewsPageRequest } from '../../models/shelves/getBookReviewsPageRequest';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Pagination } from '../../models/_shared/pagination';
import { ReviewPreview } from '../../models/shelves/reviewPreview';
import { ReviewStatistics } from '../../models/shelves/reviewStatistics';

@Injectable()
export class ReviewService {
  private COMPONENT = `${environment.api_main}/api/shelf/review`;

  constructor(private connection: HttpClient) {}

  public getPage(
    request: GetBookReviewsPageRequest
  ): Observable<Pagination<ReviewPreview>> {
    const url = `${environment.api_main}/api/composition/review/reviews`;
    return this.connection.post<Pagination<ReviewPreview>>(url, request);
  }

  public getStatistics(bookId: string): Observable<ReviewStatistics> {
    const url = `${this.COMPONENT}/statistics/${bookId}`;
    return this.connection.get<ReviewStatistics>(url);
  }
}
