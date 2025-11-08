import { CommonModule } from '@angular/common';
import { Component, Input, OnChanges, OnDestroy } from '@angular/core';
import { MediaFileService } from '../../../../business/services/media/media-file.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';

@Component({
  standalone: true,
  selector: 'media-image',
  imports: [CommonModule],
  templateUrl: './media-image.component.html',
  styleUrl: './media-image.component.scss',
})
export class MediaImageComponent implements OnChanges, OnDestroy {
  @Input() aspectRatio: string | undefined;
  @Input() objectFit: string | undefined;
  @Input({ required: true }) fileId!: MediaFileId | undefined;

  public imageUrl: SafeUrl | undefined;

  constructor(
    private fileService: MediaFileService,
    private sanitizer: DomSanitizer
  ) {}

  public ngOnDestroy(): void {
    if (this.imageUrl) {
      this.disposeOfImage();
    }
  }

  public ngOnChanges(): void {
    if (this.imageUrl) {
      this.disposeOfImage();
    }

    if (this.fileId) {
      this.downloadImage(this.fileId);
    }
  }

  private disposeOfImage(): void {
    URL.revokeObjectURL(this.imageUrl as string);
    this.imageUrl = undefined;
  }

  private downloadImage(file: MediaFileId): void {
    this.fileService.download(file).subscribe((blob) => {
      if (!blob?.type || !blob.type.startsWith('image/')) {
        this.imageUrl = undefined;
        return;
      }

      const url = URL.createObjectURL(blob);
      this.imageUrl = this.sanitizer.bypassSecurityTrustUrl(url);
    });
  }
}
