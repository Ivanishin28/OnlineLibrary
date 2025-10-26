import { Component, OnInit } from '@angular/core';
import { AuthorPreview } from '../../../../business/models/books/apiModels/authorPreview';
import { PersonalAuthorsService } from '../../../../business/services/books/personal-authors.service';
import { CommonModule } from '@angular/common';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { AuthorCreationWindowManager } from '../../../../business/managers/windows/authorCreationWindowManager';
import { ButtonModule } from 'primeng/button';

@Component({
  standalone: true,
  selector: 'authors-controls',
  imports: [CommonModule, DynamicDialogModule, ButtonModule],
  providers: [AuthorCreationWindowManager, PersonalAuthorsService],
  templateUrl: './authors-controls.component.html',
  styleUrl: './authors-controls.component.scss',
})
export class AuthorsControlsComponent implements OnInit {
  public authors: AuthorPreview[] = [];

  constructor(
    private personalAuthorsService: PersonalAuthorsService,
    private authorCreationWindowManager: AuthorCreationWindowManager
  ) {}

  public ngOnInit(): void {
    this.loadAuthors();
  }

  public createAuthor(): void {
    this.authorCreationWindowManager.createAuthor().subscribe((result) => {
      if (result.isSuccess) {
        this.loadAuthors();
      }
    });
  }

  public delete(authorId: string): void {
    // Note: Delete functionality would need to be implemented in PersonalAuthorsService
    // For now, this is a placeholder
    console.log('Delete author:', authorId);
  }

  private loadAuthors(): void {
    this.personalAuthorsService.getPersonalAuthors().subscribe((authors) => {
      this.authors = authors;
    });
  }
}
