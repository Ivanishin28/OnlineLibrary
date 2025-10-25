import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { UploadFileRequest } from '../../models/media/uploadFileRequest';
import { BusinessError } from '../../models/_shared/businessError';

@Injectable()
export class MediaFileService {
  private COMPONENT = `${environment.api_main}/api/media/mediafile`;

  constructor(private connection: HttpClient) {}

  public get(fileId: string): Observable<any> {
    const url = `${this.COMPONENT}/${fileId}`;
    return this.connection.get(url);
  }

  public upload(request: UploadFileRequest): Observable<Result<string>> {
    const url = `${this.COMPONENT}/upload`;
    return this.connection
      .post<string | undefined>(url, request)
      .pipe(
        map((x) =>
          !x || x == ''
            ? Result.failure([new BusinessError('MediaFile', 'MediaFileError')])
            : Result.success(x)
        )
      );
  }
}
