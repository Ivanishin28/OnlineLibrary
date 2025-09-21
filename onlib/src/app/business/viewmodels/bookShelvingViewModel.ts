import { Shelf } from '../models/shelves/shelf';

export class BookShelvingViewModel {
  private _selectedShelf: Shelf | undefined;

  constructor(public shelves: Shelf[]) {}

  public get selectedShelf(): Shelf | undefined {
    return this.selectedShelf;
  }

  public selectShelf(id: string): void {
    this._selectedShelf = this.shelves.find((x) => x.id);
  }
}
