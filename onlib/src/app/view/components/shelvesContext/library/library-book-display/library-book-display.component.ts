import { Component, Input } from '@angular/core';
import { LibraryShelvedBook } from '../../../../../business/models/shelves/libraryShelvedBook';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'library-book-display',
  imports: [CommonModule, TableModule, RouterModule],
  templateUrl: './library-book-display.component.html',
  styleUrl: './library-book-display.component.scss',
})
export class LibraryBookDisplayComponent {
  @Input({ required: true }) books!: LibraryShelvedBook[];

  public cast(row: any): LibraryShelvedBook {
    return row as LibraryShelvedBook;
  }
}
