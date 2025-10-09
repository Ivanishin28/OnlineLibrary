export class TagSelection {
  constructor(public readonly id: string, public isSelected: boolean) {}

  public toggle(): void {
    this.isSelected = !this.isSelected;
  }
}
