import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Genre } from '../../models/shelves/genre';

@Injectable()
export class GenreService {
  private CONTROLLER = `${environment.api_main}/api/book/genre`;

  constructor(private connection: HttpClient) {}

  public getAll(): Observable<Genre[]> {
    const url = `${this.CONTROLLER}/all`;
    return this.connection.get<Genre[]>(url);
  }
}
