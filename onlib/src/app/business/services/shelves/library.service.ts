import { Injectable } from '@angular/core';
import { UserId } from '../../models/_shared/userId';
import { Observable } from 'rxjs';
import { LibrarySummary } from '../../models/shelves/librarySummary';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Page } from '../../models/_shared/page';
import { LibraryFilter } from '../../models/shelves/libraryFilter';
import { GetLibraryPageRequest } from '../../models/shelves/getLibraryPageRequest';

@Injectable()
export class LibraryService {
  constructor(private connection: HttpClient) {}

  public getLibrarySummaryBy(
    userId: UserId
  ): Observable<LibrarySummary | undefined> {
    const url = `${environment.api_main}/api/shelf/shelvedbook/summary/${userId.value}`;
    return this.connection.get<LibrarySummary>(url);
  }

  public getLibraryPage(request: GetLibraryPageRequest): Observable<any> {
    const url = `${environment.api_main}/api/composition/library/page`;
    return this.connection.post<any>(url, request);
  }
}
