import { Routes } from '@angular/router';
import { BooksPageComponent } from './view/components/booksPage/books-page/books-page.component';
import { RegisterComponent } from './view/components/account/register/register.component';
import { LoginComponent } from './view/components/account/login/login.component';
import { ShelvesControlsComponent } from './view/components/shelvesContext/shelves-controls/shelves-controls.component';
import { BookPageComponent } from './view/components/books/book-page/book-page.component';

export const routes: Routes = [
  { path: '', component: BooksPageComponent },
  { path: 'books', component: BooksPageComponent },
  { path: 'books/:id', component: BookPageComponent },
  { path: 'account/register', component: RegisterComponent },
  { path: 'account/login', component: LoginComponent },
  { path: 'shelves', component: ShelvesControlsComponent },
];
