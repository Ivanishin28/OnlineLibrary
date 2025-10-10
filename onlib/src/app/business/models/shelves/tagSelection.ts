import { BookTag } from './bookTag';
import { Tag } from './tag';

export class TagSelection {
  constructor(
    public shelvedBookId: string,
    public readonly options: TagSelectionOption[]
  ) {}

  public static from(
    shelvedBookId: string,
    userTags: Tag[],
    selectedTags: BookTag[]
  ): TagSelection {
    const options = userTags.map((x) => {
      const isSelected = selectedTags.some(
        (selectedTag) => selectedTag.tag_id == x.id
      );

      return new TagSelectionOption(x, isSelected);
    });

    console.log(options);

    return new TagSelection(shelvedBookId, options);
  }
}

export class TagSelectionOption {
  constructor(public readonly tag: Tag, public isSelected: boolean) {}

  public set(): void {
    this.isSelected = true;
  }

  public reset(): void {
    this.isSelected = false;
  }
}
