import { MediaFileId } from '../models/_shared/mediaFileId';

export class FileUploadViewModel {
  private _file: MediaFileId | undefined;

  public get file(): MediaFileId | undefined {
    return this._file;
  }

  public upload(file: MediaFileId): void {
    this._file = file;
  }

  public remove(): void {
    this._file = undefined;
  }
}
