import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { BookPreview } from '../../models/books/bookPreview';
import { environment } from '../../../environments/environment';
import { ApiResult } from '../../models/_shared/apiResult';
import { fromApiResult } from '../mappings/fromApiResult';

@Injectable()
export class BookService {
  private readonly COMPONENT: string = 'api/book/Book';

  constructor(private http: HttpClient) {}

  public getAll(): Observable<BookPreview[]> {
    const url = `${environment.api_main}/${this.COMPONENT}/all`;

    return this.http
      .get<ApiResult<BookPreview[]>>(url)
      .pipe(map((api) => fromApiResult(api)));
  }
}
