import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth/auth.service';
import { BookPreview } from '../../models/books/bookPreview';
import { map, Observable, switchMap, take } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { ApiResult } from '../../models/_shared/apiResult';
import { Result } from '../../models/_shared/result';
import { resultFromApiResult } from '../mappings/fromApiResult';

@Injectable()
export class PersonalBooksService {
  private readonly COMPONENT: string = `${environment.api_main}/api/book/Book`;

  constructor(private connection: HttpClient, private auth: AuthService) {}

  public getPersonalBooks(): Observable<BookPreview[]> {
    return this.auth.loggedUser$.pipe(
      take(1),
      switchMap((x) => {
        const url = `${this.COMPONENT}/userId/${x.userId.value}`;
        return this.connection.get<BookPreview[]>(url);
      })
    );
  }

  public delete(bookId: string): Observable<Result<void>> {
    const url = `${this.COMPONENT}/delete/${bookId}`;
    return this.connection
      .delete<ApiResult<void>>(url)
      .pipe(map((x) => resultFromApiResult(x)));
  }
}
