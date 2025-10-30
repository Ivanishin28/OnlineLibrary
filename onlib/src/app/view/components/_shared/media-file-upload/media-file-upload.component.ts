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

  @Output() fileUploaded: EventEmitter<MediaFileId> =
    new EventEmitter<MediaFileId>();

  constructor(private mediaFileService: MediaFileService) {}

  public upload(model: UploadEvent): void {
    const file = model.files[0];
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
}
