import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import {
  TagSelection,
  TagSelectionOption,
} from '../../../../../business/models/shelves/tagSelection';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { ShelvedBookService } from '../../../../../business/services/shelves/shelvedBook.service';
import { map, Observable, tap } from 'rxjs';
import { Result } from '../../../../../business/models/_shared/result';

@Component({
  selector: 'tag-selection-panel',
  imports: [CommonModule, CheckboxModule, FormsModule],
  templateUrl: './tag-selection-panel.component.html',
  styleUrl: './tag-selection-panel.component.scss',
})
export class TagSelectionPanelComponent {
  @Input({ required: true }) tagSelection!: TagSelection;

  constructor(private shelvedBookService: ShelvedBookService) {}

  public toggle(tag: TagSelectionOption): void {
    const operation = !tag.isSelected ? this.addTag(tag) : this.removeTag(tag);
    operation.subscribe((x) => {
      if (!x.isSuccess) {
        console.log('something went wrong');
      }
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
