import { Routes } from '@angular/router';
import { BooksPageComponent } from './view/components/booksPage/books-page/books-page.component';
import { RegisterComponent } from './view/components/account/register/register.component';
import { LoginComponent } from './view/components/account/login/login.component';
import { BookPageComponent } from './view/components/books/book-page/book-page.component';
import { loggedUserGuard } from './view/routeGuards/logged-user.guard';
import { LibraryOrganizationComponent } from './view/components/shelvesContext/library-organization/library-organization.component';
import { LibraryComponent } from './view/components/shelvesContext/library/library.component';
import { ApplicationEntranceComponent } from './view/components/account/application-entrance/application-entrance.component';
import { ApplicationAccessComponent } from './view/components/application-access/application-access.component';

export const routes: Routes = [
  {
    path: 'account',
    component: ApplicationEntranceComponent,
    children: [
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
    ],
  },
  {
    path: '',
    component: ApplicationAccessComponent,
    canActivate: [loggedUserGuard],
    children: [
      { path: '', component: BooksPageComponent },
      { path: 'books', component: BooksPageComponent },
      { path: 'books/:id', component: BookPageComponent },
      { path: 'organization', component: LibraryOrganizationComponent },
      { path: 'library/:userId', component: LibraryComponent },
    ],
  },
];
