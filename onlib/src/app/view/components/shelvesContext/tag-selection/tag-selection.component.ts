import { Component, Input, OnInit } from '@angular/core';
import { ShelvedBookService } from '../../../../business/services/shelves/shelvedBook.service';
import { TagService } from '../../../../business/services/shelves/tag.service';
import { forkJoin } from 'rxjs';
import { ShelvedBook } from '../../../../business/models/shelves/shelvedBook';
import { TagSelection } from '../../../../business/models/shelves/tagSelection';
import { PopoverModule } from 'primeng/popover';
import { TagSelectionPanelComponent } from './tag-selection-panel/tag-selection-panel.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'tag-selection',
  imports: [CommonModule, PopoverModule, TagSelectionPanelComponent],
  templateUrl: './tag-selection.component.html',
  styleUrl: './tag-selection.component.scss',
})
export class TagSelectionComponent implements OnInit {
  @Input({ required: true }) shelvedBook: ShelvedBook | undefined;

  public tags: TagSelection[] | undefined;

  constructor(
    private tagService: TagService,
    private shelvedBookService: ShelvedBookService
  ) {}

  public ngOnInit(): void {}

  public loadTags(): void {
    if (!this.shelvedBook) {
      return;
    }

    forkJoin({
      userTags: this.tagService.getPersonalTags(),
      shelvedBook: this.shelvedBookService.get(this.shelvedBook.book_id),
    }).subscribe(({ userTags, shelvedBook }) => {
      if (!shelvedBook) {
        return;
      }

      this.tags = userTags.map((x) => {
        const isSelected = userTags.some((userTag) =>
          shelvedBook.tags.some(
            (shelvedBookTag) => shelvedBookTag.id == userTag.id
          )
        );

        return new TagSelection(x, isSelected);
      });
    });
  }

  public clearTags(): void {
    this.tags = undefined;
  }
}
