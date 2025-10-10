import { Tag } from './tag';

export class TagSelection {
  constructor(public readonly tag: Tag, public isSelected: boolean) {}

  public toggle(): void {
    this.isSelected = !this.isSelected;
  }
}
