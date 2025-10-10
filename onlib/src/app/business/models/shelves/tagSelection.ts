import { Tag } from './tag';

export class TagSelection {
  constructor(
    public shelvedBookId: string,
    public readonly options: TagSelectionOption[]
  ) {}

  public static from(
    shelvedBookId: string,
    userTags: Tag[],
    selectedTags: Tag[]
  ): TagSelection {
    const options = userTags.map((x) => {
      const isSelected = userTags.some((userTag) =>
        selectedTags.some((shelvedBookTag) => shelvedBookTag.id == userTag.id)
      );

      return new TagSelectionOption(x, isSelected);
    });

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
