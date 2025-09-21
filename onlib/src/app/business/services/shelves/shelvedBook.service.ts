import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserId } from '../../models/_shared/userId';
import { Observable } from 'rxjs';
import { ShelvedBook } from '../../models/shelves/shelvedBook';
import { environment } from '../../../environments/environment';

@Injectable()
export class ShelvedBookService {
  private readonly CONTROLLER = `${environment.api_main}/api/shelf/shelvedbook`;

  constructor(private connection: HttpClient) {}

  public get(
    userId: UserId,
    bookId: string
  ): Observable<ShelvedBook | undefined> {
    const url = `${this.CONTROLLER}/user/${userId.value}/book/${bookId}`;
    return this.connection.get<ShelvedBook | undefined>(url);
  }
}
