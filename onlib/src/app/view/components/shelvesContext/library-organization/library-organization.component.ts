import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule, ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { UserId } from '../../../../business/models/_shared/userId';
import { take } from 'rxjs';
import { ShelvesControlsComponent } from '../shelves-controls/shelves-controls.component';
import { TagsControlsComponent } from '../tags-controls/tags-controls.component';
import { AuthorsControlsComponent } from '../../books/authors-controls/authors-controls.component';
import { BooksControlsComponent } from '../../books/books-controls/books-controls.component';

@Component({
  standalone: true,
  selector: 'library-organization',
  imports: [
    CommonModule,
    RouterModule,
    ShelvesControlsComponent,
    TagsControlsComponent,
    AuthorsControlsComponent,
    BooksControlsComponent,
  ],
  templateUrl: './library-organization.component.html',
  styleUrl: './library-organization.component.scss',
})
export class LibraryOrganizationComponent implements OnInit {
  public activeSection: string = 'shelves';
  public userId: UserId | undefined;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private auth: AuthService
  ) {}

  public ngOnInit(): void {
    this.auth.loggedUser$
      .pipe(take(1))
      .subscribe((x) => (this.userId = x.userId));

    const updateActiveSection = () => {
      const url = this.router.url;
      if (url.includes('/organization/tags')) {
        this.activeSection = 'tags';
      } else if (url.includes('/organization/authors')) {
        this.activeSection = 'authors';
      } else if (url.includes('/organization/books')) {
        this.activeSection = 'books';
      } else if (url.includes('/organization/shelves') || url === '/organization') {
        this.activeSection = 'shelves';
      }
    };

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => updateActiveSection());

    updateActiveSection();
  }
}
