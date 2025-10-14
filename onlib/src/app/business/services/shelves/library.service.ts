import { Injectable } from '@angular/core';
import { UserId } from '../../models/_shared/userId';
import { Observable } from 'rxjs';
import { LibrarySummary } from '../../models/shelves/librarySummary';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class LibraryService {
  constructor(private connection: HttpClient) {}

  public getLibrarySummaryBy(
    userId: UserId
  ): Observable<LibrarySummary | undefined> {
    const url = `${environment.api_main}/api/shelf/shelvedbook/summary/${userId.value}`;
    return this.connection.get<LibrarySummary>(url);
  }
}
