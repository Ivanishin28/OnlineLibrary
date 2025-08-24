import { Component } from '@angular/core';
import { NavbarComponent } from '../../header/navbar/navbar.component';

@Component({
  selector: 'books-page',
  imports: [NavbarComponent],
  templateUrl: './books-page.component.html',
  styleUrl: './books-page.component.scss',
})
export class BooksPageComponent {}
