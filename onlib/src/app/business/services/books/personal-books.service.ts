import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth/auth.service';
import { BookPreview } from '../../models/books/bookPreview';
import { Observable, switchMap, take } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Injectable } from '@angular/core';

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
}
