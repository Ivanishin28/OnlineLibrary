import { Injectable } from '@angular/core';
import { MediaFileId } from '../../models/_shared/mediaFileId';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ProfileService {
  private CONTROLLER = `${environment.api_main}/api/identity/applicationuser`;

  constructor(private connection: HttpClient) {}

  public updateAvatar(avatar: MediaFileId): Observable<void> {
    const url = `${this.CONTROLLER}/set-avatar`;
    return this.connection.post<void>(url, { avatar_id: avatar.value });
  }
}
