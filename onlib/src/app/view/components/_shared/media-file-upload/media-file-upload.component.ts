import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FileUploadModule } from 'primeng/fileupload';
import { MediaFileService } from '../../../../business/services/media/media-file.service';

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
        .subscribe(console.log);
    };

    reader.readAsDataURL(file);
  }
}
