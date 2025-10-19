import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  standalone: true,
  selector: 'application-entrance',
  imports: [ButtonModule, RouterOutlet, RouterModule],
  templateUrl: './application-entrance.component.html',
  styleUrl: './application-entrance.component.scss',
})
export class ApplicationEntranceComponent {}
