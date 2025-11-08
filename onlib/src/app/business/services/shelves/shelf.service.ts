import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ShelfForBook } from '../../models/shelves/shelfForBook';

@Injectable()
export class ShelfService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/shelvedbook`;

  constructor(private connection: HttpClient) {}

  public getShelvedCount(bookId: string): Observable<number> {
    const url = `${this.CONTROLLER}/book/${bookId}/shelved-count`;
    return this.connection.get<number>(url);
  }

  public getAllShelfsForBook(bookId: string): Observable<ShelfForBook[]> {
    const url = `${this.CONTROLLER}/book/${bookId}/shelfs`;
    return this.connection.get<ShelfForBook[]>(url);
  }
}

