import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';
import { ProfileWindowComponent } from '../../../view/components/user/profile-window/profile-window.component';

@Injectable()
export class ProfileWindowManager {
  constructor(private dialog: DialogService) {}

  public open(): Observable<void> {
    const ref = this.dialog.open(ProfileWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Profile',
      modal: true,
    });

    return ref.onClose;
  }
}

