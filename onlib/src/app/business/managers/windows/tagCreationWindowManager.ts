import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { filter, map, Observable, switchMap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { TagService } from '../../services/shelves/tag.service';
import { TagCreationWindowOutput } from '../../models/shelves/tagCreationWindowOutput';
import { CreateTagRequest } from '../../models/shelves/createTagRequest';
import { TagCreationWindowComponent } from '../../../view/components/shelvesContext/tag-creation-window/tag-creation-window.component';

@Injectable()
export class ShelfCreationWindowManager {
  constructor(private dialog: DialogService, private tagService: TagService) {}

  public createShelf(): Observable<Result<void>> {
    const ref = this.dialog.open(TagCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Create Shelf',
    });

    return ref.onClose.pipe(
      filter((output: TagCreationWindowOutput | undefined) => !!output),
      switchMap((output: TagCreationWindowOutput) => {
        const request: CreateTagRequest = {
          name: output.name,
        };
        return this.tagService.create(request).pipe(map((x) => x.toVoid()));
      })
    );
  }
}
