import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { filter, map, Observable, switchMap, take } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { TagService } from '../../services/shelves/tag.service';
import { TagCreationWindowOutput } from '../../models/shelves/tagCreationWindowOutput';
import { CreateTagRequest } from '../../models/shelves/createTagRequest';
import { TagCreationWindowComponent } from '../../../view/components/shelvesContext/tag-creation-window/tag-creation-window.component';
import { AuthService } from '../../services/auth/auth.service';
import { UserId } from '../../models/_shared/userId';

@Injectable()
export class TagCreationWindowManager {
  constructor(
    private dialog: DialogService,
    private tagService: TagService,
    private authService: AuthService
  ) {}

  public createTag(): Observable<Result<void>> {
    return this.authService.loggedUser$.pipe(
      take(1),
      switchMap((x) => this.openCreateTagWindow(x.userId))
    );
  }

  private openCreateTagWindow(userId: UserId) {
    const ref = this.dialog.open(TagCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Create Shelf',
      data: userId,
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
