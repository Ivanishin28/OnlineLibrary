import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { ShelfService } from '../../services/shelves/shelf.service';
import { filter, map, Observable, switchMap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { ShelfCreationWindowComponent } from '../../../view/components/shelvesContext/shelf-creation-window/shelf-creation-window.component';
import { ShelfCreationWindowOutput } from '../../models/shelves/shelfCreationWindowOutput';
import { CreateShelfRequest } from '../../models/shelves/createShelfRequest';
import { UserId } from '../../models/_shared/userId';

@Injectable()
export class ShelfCreationWindowManager {
  constructor(
    private dialog: DialogService,
    private shelfService: ShelfService
  ) {}

  public createShelfFor(userId: UserId): Observable<Result<void>> {
    const ref = this.dialog.open(ShelfCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Create Shelf',
    });

    return ref.onClose.pipe(
      filter((output: ShelfCreationWindowOutput | undefined) => !!output),
      switchMap((output: ShelfCreationWindowOutput) => {
        const request = new CreateShelfRequest(userId.value, output.name);
        return this.shelfService.create(request).pipe(map((x) => x.toVoid()));
      })
    );
  }
}
