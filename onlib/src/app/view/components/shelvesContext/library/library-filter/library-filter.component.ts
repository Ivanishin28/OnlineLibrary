import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { LibrarySummary } from '../../../../../business/models/shelves/librarySummary';
import { LibraryFilter } from '../../../../../business/models/shelves/libraryFilter';
import { TagSummary } from '../../../../../business/models/shelves/tagSummary';
import { ShelfSummary } from '../../../../../business/models/shelves/shelfSummary';

@Component({
  selector: 'library-filter',
  imports: [CommonModule],
  templateUrl: './library-filter.component.html',
  styleUrl: './library-filter.component.scss',
})
export class LibraryFilterComponent {
  @Input({ required: true }) summary!: LibrarySummary;

  @Output() itemSelected: EventEmitter<LibraryFilter> =
    new EventEmitter<LibraryFilter>();

  public selectAll(): void {
    this.itemSelected.emit({ tagId: undefined, shelfId: undefined });
  }

  public selectTag(tag: TagSummary): void {
    this.itemSelected.emit({ tagId: tag.id, shelfId: undefined });
  }

  public selectShelf(shelf: ShelfSummary): void {
    this.itemSelected.emit({ shelfId: shelf.id, tagId: undefined });
  }
}
