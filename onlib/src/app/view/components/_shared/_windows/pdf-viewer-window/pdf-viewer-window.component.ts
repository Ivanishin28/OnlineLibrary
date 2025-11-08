import { Component, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { PdfViewerWindowInput } from '../../../../../business/models/_shared/pdfViewerWindowInput';
import { DynamicDialogConfig } from 'primeng/dynamicdialog';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { CommonModule } from '@angular/common';
import { MediaFileService } from '../../../../../business/services/media/media-file.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'pdf-viewer-window',
  imports: [CommonModule, PdfViewerModule],
  templateUrl: './pdf-viewer-window.component.html',
  styleUrl: './pdf-viewer-window.component.scss',
})
export class PdfViewerWindowComponent implements OnInit, OnDestroy {
  private input: PdfViewerWindowInput;
  public url: SafeUrl | undefined;

  constructor(
    private sanitizer: DomSanitizer,
    private fileService: MediaFileService,
    config: DynamicDialogConfig
  ) {
    this.input = config.data;
  }

  public ngOnDestroy(): void {
    if (this.url) {
      this.disposeOfImage();
    }
  }

  public ngOnInit(): void {
    this.fileService.download(this.input.file).subscribe((blob) => {
      const url = URL.createObjectURL(blob);
      this.url = this.sanitizer.bypassSecurityTrustUrl(url);
    });
  }

  private disposeOfImage(): void {
    URL.revokeObjectURL(this.url as string);
    this.url = undefined;
  }
}
