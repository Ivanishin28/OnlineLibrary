import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../header/navbar/navbar.component';

@Component({
  selector: 'application-access',
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './application-access.component.html',
  styleUrl: './application-access.component.scss',
})
export class ApplicationAccessComponent {}
