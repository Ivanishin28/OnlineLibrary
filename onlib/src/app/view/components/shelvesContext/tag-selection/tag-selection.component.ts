import { Component, Input, OnInit } from '@angular/core';
import { ShelvedBookService } from '../../../../business/services/shelves/shelvedBook.service';
import { TagService } from '../../../../business/services/shelves/tag.service';
import { forkJoin } from 'rxjs';
import { UserId } from '../../../../business/models/_shared/userId';
import { ShelvedBook } from '../../../../business/models/shelves/shelvedBook';
import { TagSelection } from '../../../../business/models/shelves/tagSelection';

@Component({
  selector: 'tag-selection',
  imports: [],
  templateUrl: './tag-selection.component.html',
  styleUrl: './tag-selection.component.scss',
})
export class TagSelectionComponent implements OnInit {
  @Input({ required: true }) shelvedBook!: ShelvedBook;

  public tags: TagSelection[] | undefined;

  constructor(
    private tagService: TagService,
    private shelvedBookService: ShelvedBookService
  ) {}

  public ngOnInit(): void {}

  private load(): void {
    forkJoin({
      userTags: this.tagService.getPersonalTags(),
      shelvedBook: this.shelvedBookService.get(this.shelvedBook!.book_id),
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

        return new TagSelection(x.id, isSelected);
      });
    });
  }
}
