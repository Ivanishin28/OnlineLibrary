import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FileUploadModule } from 'primeng/fileupload';
import { MediaFileService } from '../../../../business/services/media/media-file.service';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';

interface UploadEvent {
  originalEvent: Event;
  files: File[];
}

@Component({
  standalone: true,
  selector: 'media-file-upload',
  imports: [CommonModule, FileUploadModule],
  templateUrl: './media-file-upload.component.html',
  styleUrl: './media-file-upload.component.scss',
})
export class MediaFileUploadComponent {
  @Input({ required: true }) uploadedFileId: string | undefined;

  @Input() accept: string | null = 'image/*';

  @Input() maxFileSize: number = 1_000_000;

  @Output() fileUploaded: EventEmitter<MediaFileId> =
    new EventEmitter<MediaFileId>();

  constructor(private mediaFileService: MediaFileService) {}

  public upload(model: UploadEvent): void {
    const file = model.files?.[0];
    if (!file) {
      return;
    }

    if (this.accept && !this.isFileAllowed(file, this.accept)) {
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      const result = reader.result as string;
      const base64 = result.split(',')[1];
      this.mediaFileService
        .upload({
          content: base64,
          content_type: file.type,
        })
        .subscribe((uploadResult) => {
          if (uploadResult.isSuccess) {
            this.fileUploaded.next(new MediaFileId(uploadResult.value));
          }
        });
    };
    reader.readAsDataURL(file);
  }

  private isFileAllowed(file: File, accept: string): boolean {
    const mimeType = file.type ?? '';
    const lowerName = file.name.toLowerCase();

    const patterns = accept
      .split(',')
      .map((pattern) => pattern.trim())
      .filter((pattern) => pattern.length > 0);

    if (patterns.length === 0) {
      return true;
    }

    return patterns.some((pattern) => {
      if (pattern === '*/*') {
        return true;
      }

      if (pattern.startsWith('.')) {
        return lowerName.endsWith(pattern.toLowerCase());
      }

      if (pattern.endsWith('/*')) {
        const prefix = pattern.slice(0, -1);
        return mimeType.startsWith(prefix);
      }

      return mimeType === pattern;
    });
  }
}
