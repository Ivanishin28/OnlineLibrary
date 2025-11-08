import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthorPreview } from '../../models/books/apiModels/authorPreview';
import { environment } from '../../../environments/environment';

@Injectable()
export class AuthorService {
  private readonly COMPONENT: string = 'api/book/Author';

  constructor(private http: HttpClient) {}

  public search(query: string): Observable<AuthorPreview[]> {
    const url = `${environment.api_main}/${this.COMPONENT}/search?query=${query}`;
    return this.http.get<AuthorPreview[]>(url);
  }
}
