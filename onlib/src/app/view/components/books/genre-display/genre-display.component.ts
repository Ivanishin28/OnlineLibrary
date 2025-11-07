import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Genre } from '../../../../business/models/shelves/genre';

@Component({
  selector: 'genre-display',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './genre-display.component.html',
  styleUrl: './genre-display.component.scss',
})
export class GenreDisplayComponent {
  @Input({ required: true }) genres!: Genre[];
}
