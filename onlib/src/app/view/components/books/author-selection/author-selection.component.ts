import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AuthorPreview } from '../../../../business/models/books/apiModels/authorPreview';
import { AuthorSearchComponent } from '../author-search/author-search.component';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'author-selection',
  imports: [CommonModule, AuthorSearchComponent],
  templateUrl: './author-selection.component.html',
  styleUrl: './author-selection.component.scss',
})
export class AuthorSelectionComponent {
  @Input({ required: true }) selectedAuthors!: AuthorPreview[];
  @Output() selectedAuthorsChange: EventEmitter<AuthorPreview[]> =
    new EventEmitter<AuthorPreview[]>();

  public selectAuthor(author: AuthorPreview): void {
    const exists = this.selectedAuthors.some((x) => x.id === author.id);
    if (exists) {
      return;
    }

    const updated = [...this.selectedAuthors, author];
    this.selectedAuthorsChange.emit(updated);
  }

  public removeAuthor(author: AuthorPreview): void {
    const updated = this.selectedAuthors.filter((x) => x.id !== author.id);
    this.selectedAuthorsChange.emit(updated);
  }
}
