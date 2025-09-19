import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ShelfPreview as ShelfPreview } from '../../models/bookshelves/shelfPreview';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable()
export class ShelfService {
  constructor(private connection: HttpClient) {}

  public getByUserId(userId: string): Observable<ShelfPreview[]> {
    const url = `${environment.api_main}/api/shelf/shelf/user/${userId}`;

    return this.connection.get<ShelfPreview[]>(url);
  }
}