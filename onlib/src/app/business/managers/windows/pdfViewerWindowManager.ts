import { Observable } from 'rxjs';
import { MediaFileId } from '../../models/_shared/mediaFileId';
import { DialogService } from 'primeng/dynamicdialog';
import { PdfViewerWindowComponent } from '../../../view/components/_shared/_windows/pdf-viewer-window/pdf-viewer-window.component';
import { PdfViewerWindowInput } from '../../models/_shared/pdfViewerWindowInput';
import { Injectable } from '@angular/core';

@Injectable()
export class PdfViewerWindowManager {
  constructor(private dialog: DialogService) {}

  public open(header: string, file: MediaFileId): Observable<void> {
    const input: PdfViewerWindowInput = {
      file: file,
    };
    const ref = this.dialog.open(PdfViewerWindowComponent, {
      data: input,
      focusOnShow: false,
      header: header,
      modal: true,
      closable: true,
      styleClass: 'pdf-viewer-window',
    });

    return ref.onClose;
  }
}
