import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';
import { MessageWindowInput } from '../../models/_shared/messageWindowInput';
import { MessageWindowComponent } from '../../../view/components/_shared/message-window/message-window.component';

@Injectable()
export class MessageWindowManager {
  constructor(private dialog: DialogService) {}

  public show(header: string, message: string): Observable<void> {
    const ref = this.dialog.open(MessageWindowComponent, {
      closable: true,
      showHeader: true,
      header: header,
      modal: true,
      data: new MessageWindowInput(header, message),
    });

    return ref.onClose;
  }
}
