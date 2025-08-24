import { Routes } from '@angular/router';
import { BooksPageComponent } from './view/components/booksPage/books-page/books-page.component';

export const routes: Routes = [
  { path: '', component: BooksPageComponent },
  { path: 'books', component: BooksPageComponent },
];
