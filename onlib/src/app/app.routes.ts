import { Routes } from '@angular/router';
import { BooksPageComponent } from './view/components/booksPage/books-page/books-page.component';
import { RegisterComponent } from './view/components/account/register/register.component';
import { LoginComponent } from './view/components/account/login/login.component';

export const routes: Routes = [
  { path: '', component: BooksPageComponent },
  { path: 'books', component: BooksPageComponent },
  { path: 'account/register', component: RegisterComponent },
  { path: 'account/login', component: LoginComponent },
];
