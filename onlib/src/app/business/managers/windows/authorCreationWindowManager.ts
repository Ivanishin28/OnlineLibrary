import { Injectable } from '@angular/core';
import { DialogService } from 'primeng/dynamicdialog';
import { filter, map, Observable, switchMap } from 'rxjs';
import { Result } from '../../models/_shared/result';
import { CreateAuthorRequest } from '../../models/books/requests/createAuthorRequest';
import { UpdateAuthorRequest } from '../../models/books/requests/updateAuthorRequest';
import { PersonalAuthorsService } from '../../services/books/personal-authors.service';
import { AuthorCreationWindowOutput } from '../../models/books/authorCreationWindowOutput';
import { AuthorCreationWindowInput } from '../../models/books/authorCreationWindowInput';
import { AuthorCreationWindowComponent } from '../../../view/components/books/author-creation-window/author-creation-window.component';

@Injectable()
export class AuthorCreationWindowManager {
  constructor(
    private dialog: DialogService,
    private personalAuthorsService: PersonalAuthorsService
  ) {}

  public createAuthor(): Observable<Result<string>> {
    const ref = this.dialog.open(AuthorCreationWindowComponent, {
      closable: true,
      showHeader: true,
      header: 'Create Author',
      modal: true,
    });

    return ref.onClose.pipe(
      filter((output: AuthorCreationWindowOutput | undefined) => !!output),
      switchMap((output: AuthorCreationWindowOutput) => {
        const request: CreateAuthorRequest = {
          first_name: output.first_name,
          last_name: output.last_name,
          birth_date: output.birth_date,
          avatar_id: output.avatarId,
          biography: output.biography
        };
        return this.personalAuthorsService.createAuthor(request);
      })
    );
  }

  public edit(authorId: string): Observable<Result<void>> {
    return this.personalAuthorsService.getFull(authorId).pipe(
      switchMap((author) => {
        if (!author) {
          throw new Error('Author not found');
        }

        const ref = this.dialog.open(AuthorCreationWindowComponent, {
          closable: true,
          showHeader: true,
          header: 'Edit Author',
          modal: true,
          data: {
            input: new AuthorCreationWindowInput(author),
          },
        });

        return ref.onClose.pipe(
          filter((output: AuthorCreationWindowOutput | undefined) => !!output),
          switchMap((output: AuthorCreationWindowOutput) => {
            const request: UpdateAuthorRequest = {
              id: authorId,
              birth_date: output.birth_date,
              avatar_id: output.avatarId ?? '',
              biography: output.biography,
            };
            return this.personalAuthorsService.update(request);
          })
        );
      })
    );
  }
}
