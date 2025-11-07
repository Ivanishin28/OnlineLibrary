import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BookPreview } from '../../models/books/bookPreview';
import { FullBook } from '../../models/books/fullBook';
import { environment } from '../../../environments/environment';
import { ApiResult } from '../../models/_shared/apiResult';
import {
  resultFromApiResult,
  valueFromApiResult,
} from '../mappings/fromApiResult';
import { CreateBookRequest } from '../../models/books/createBookRequest';
import { UpdateBookRequest } from '../../models/books/updateBookRequest';
import { Result } from '../../models/_shared/result';
import { BookFilter } from '../../models/shelves/bookFilter';
import { Page } from '../../models/_shared/page';
import { Pagination } from '../../models/_shared/pagination';

@Injectable()
export class BookService {
  private readonly CONTROLLER: string = 'api/book/Book';

  constructor(private http: HttpClient) {}

  public getFull(id: string): Observable<FullBook> {
    const url = `${environment.api_main}/${this.CONTROLLER}/full/${id}`;

    return this.http.get<FullBook>(url);
  }

  public create(request: CreateBookRequest): Observable<Result<void>> {
    const url = `${environment.api_main}/${this.CONTROLLER}/create`;

    return this.http
      .post<ApiResult<void>>(url, request)
      .pipe(map((api) => resultFromApiResult(api)));
  }

  public search(query: string): Observable<BookPreview[]> {
    const url = `${environment.api_main}/${this.CONTROLLER}/search?query=${query}`;
    return this.http.get<BookPreview[]>(url);
  }

  public update(request: UpdateBookRequest): Observable<Result<void>> {
    const url = `${environment.api_main}/${this.CONTROLLER}/update`;
    return this.http
      .post<ApiResult<void>>(url, request)
      .pipe(map((api) => resultFromApiResult(api)));
  }

  public getBookPage(
    filter: BookFilter,
    page: Page,
    startingAt: Date
  ): Observable<Pagination<BookPreview>> {
    const url = `${environment.api_main}/${this.CONTROLLER}/page`;
    const request = {
      filter,
      page,
      starting_at: startingAt,
    };
    return this.http.post<Pagination<BookPreview>>(url, request);
  }
}
