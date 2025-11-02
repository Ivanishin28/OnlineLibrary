import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorPreview } from '../../../../../business/models/books/apiModels/authorPreview';

@Component({
  standalone: true,
  selector: 'author-search-result',
  imports: [CommonModule],
  templateUrl: './author-search-result.component.html',
  styleUrl: './author-search-result.component.scss',
})
export class AuthorSearchResultComponent {
  @Input({ required: true }) author!: AuthorPreview;
}

