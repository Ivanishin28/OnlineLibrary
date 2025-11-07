import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Genre } from '../../../../business/models/shelves/genre';
import { GenreService } from '../../../../business/services/books/genre.service';

@Component({
  selector: 'genre-selection',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './genre-selection.component.html',
  styleUrl: './genre-selection.component.scss',
  providers: [GenreService],
})
export class GenreSelectionComponent implements OnInit {
  @Input({ required: true }) selectedGenres!: Genre[];
  @Output() selectedGenresChange: EventEmitter<Genre[]> =
    new EventEmitter<Genre[]>();

  public allGenres: Genre[] = [];

  constructor(private genreService: GenreService) {}

  public ngOnInit(): void {
    this.loadGenres();
  }

  public toggleGenre(genre: Genre): void {
    const isSelected = this.isGenreSelected(genre);
    
    if (isSelected) {
      const updated = this.selectedGenres.filter((x) => x.id !== genre.id);
      this.selectedGenresChange.emit(updated);
    } else {
      const updated = [...this.selectedGenres, genre];
      this.selectedGenresChange.emit(updated);
    }
  }

  public isGenreSelected(genre: Genre): boolean {
    return this.selectedGenres.some((x) => x.id === genre.id);
  }

  private loadGenres(): void {
    this.genreService.getAll().subscribe({
      next: (genres) => {
        this.allGenres = genres;
      },
      error: (error) => {
        console.error('Error loading genres:', error);
      },
    });
  }
}
