import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BookFilter } from '../../../../../business/models/shelves/bookFilter';
import { Genre } from '../../../../../business/models/shelves/genre';
import { GenreService } from '../../../../../business/services/books/genre.service';

@Component({
  selector: 'book-filter',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './book-filter.component.html',
  styleUrl: './book-filter.component.scss',
  providers: [GenreService],
})
export class BookFilterComponent implements OnInit {
  @Output() filterSelected: EventEmitter<BookFilter> =
    new EventEmitter<BookFilter>();

  public genres: Genre[] = [];
  public selectedIds: string[] = [];

  constructor(private genreService: GenreService) {}

  public ngOnInit(): void {
    this.loadGenres();
  }

  public toggleGenre(genre: Genre): void {
    const index = this.selectedIds.indexOf(genre.id);
    if (index > -1) {
      this.selectedIds.splice(index, 1);
    } else {
      this.selectedIds.push(genre.id);
    }
  }

  public isGenreSelected(genre: Genre): boolean {
    return this.selectedIds.includes(genre.id);
  }

  public clearFilter(): void {
    this.selectedIds = [];
    this.applyFilter();
  }

  public applyFilter(): void {
    this.filterSelected.emit({ genre_ids: this.selectedIds });
  }

  private loadGenres(): void {
    this.genreService.getAll().subscribe((x) => {
      this.genres = x;
    });
  }
}
