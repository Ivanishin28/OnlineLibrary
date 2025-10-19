import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BookPreview } from '../../models/books/bookPreview';
import { environment } from '../../../environments/environment';
import { ApiResult } from '../../models/_shared/apiResult';
import {
  resultFromApiResult,
  valueFromApiResult,
} from '../mappings/fromApiResult';
import { CreateBookRequest } from '../../models/books/createBookRequest';
import { Result } from '../../models/_shared/result';
import { UserId } from '../../models/_shared/userId';

@Injectable()
export class BookService {
  private readonly COMPONENT: string = 'api/book/Book';

  constructor(private http: HttpClient) {}

  public getFull(id: string): Observable<BookPreview> {
    const url = `${environment.api_main}/${this.COMPONENT}/full/${id}`;

    return this.http.get<BookPreview>(url);
  }

  public getByUserId(userId: UserId): Observable<BookPreview[]> {
    const url = `${environment.api_main}/${this.COMPONENT}/userId/${userId.value}`;

    return this.http.get<BookPreview[]>(url);
  }

  public getAll(): Observable<BookPreview[]> {
    const url = `${environment.api_main}/${this.COMPONENT}/all`;

    return this.http
      .get<ApiResult<BookPreview[]>>(url)
      .pipe(map((api) => valueFromApiResult(api)));
  }

  public create(request: CreateBookRequest): Observable<Result<void>> {
    const url = `${environment.api_main}/${this.COMPONENT}/create`;

    return this.http
      .post<ApiResult<void>>(url, request)
      .pipe(map((api) => resultFromApiResult(api)));
  }
}
