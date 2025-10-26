import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ShelvesControlsComponent } from '../shelves-controls/shelves-controls.component';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { UserCredentials } from '../../../../business/models/_shared/userCredentials';
import { take } from 'rxjs';
import { TagsControlsComponent } from '../tags-controls/tags-controls.component';
import { BooksControlsComponent } from '../../books/books-controls/books-controls.component';
import { AuthorsControlsComponent } from '../../books/authors-controls/authors-controls.component';

@Component({
  standalone: true,
  selector: 'library-organization',
  imports: [
    CommonModule,
    ShelvesControlsComponent,
    TagsControlsComponent,
    BooksControlsComponent,
    AuthorsControlsComponent,
  ],
  templateUrl: './library-organization.component.html',
  styleUrl: './library-organization.component.scss',
})
export class LibraryOrganizationComponent implements OnInit {
  public creds!: UserCredentials;

  constructor(private auth: AuthService) {}

  public ngOnInit(): void {
    this.auth.loggedUser$.pipe(take(1)).subscribe((x) => (this.creds = x));
  }
}
