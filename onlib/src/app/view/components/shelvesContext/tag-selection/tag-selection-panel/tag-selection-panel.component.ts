import { CommonModule } from '@angular/common';
import { Component, Input, Output } from '@angular/core';
import {
  TagSelection,
  TagSelectionOption,
} from '../../../../../business/models/shelves/tagSelection';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { ShelvedBookService } from '../../../../../business/services/shelves/shelvedBook.service';
import { map, Observable, Subject, tap } from 'rxjs';
import { Result } from '../../../../../business/models/_shared/result';

@Component({
  selector: 'tag-selection-panel',
  imports: [CommonModule, CheckboxModule, FormsModule],
  templateUrl: './tag-selection-panel.component.html',
  styleUrl: './tag-selection-panel.component.scss',
})
export class TagSelectionPanelComponent {
  @Input({ required: true }) tagSelection!: TagSelection;

  @Output() failure: Subject<void> = new Subject<void>();

  constructor(private shelvedBookService: ShelvedBookService) {}

  public toggle(tag: TagSelectionOption): void {
    const operation = !tag.isSelected ? this.addTag(tag) : this.removeTag(tag);
    operation.subscribe({
      next: (x) => {
        if (!x.isSuccess) {
          this.failure.next();
        }
      },
      error: () => {
        this.failure.next();
      },
    });
  }

  private addTag(tag: TagSelectionOption): Observable<Result<void>> {
    return this.shelvedBookService
      .addTag(this.tagSelection.shelvedBookId, tag.tag.id)
      .pipe(
        map((x) => {
          if (x.isSuccess) {
            tag.set();
          }

          return x.toVoid();
        })
      );
  }

  private removeTag(tag: TagSelectionOption): Observable<Result<void>> {
    return this.shelvedBookService
      .removeTag(this.tagSelection.shelvedBookId, tag.tag.id)
      .pipe(
        map((x) => {
          if (x.isSuccess) {
            tag.reset();
          }

          return x.toVoid();
        })
      );
  }
}
