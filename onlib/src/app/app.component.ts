import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { KnobModule } from 'primeng/knob';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './view/components/header/navbar/navbar.component';
import { Application } from './application';

@Component({
  selector: 'app-root',
  imports: [
    NavbarComponent,
    RouterOutlet,
    KnobModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [Application],
})
export class AppComponent implements OnInit {
  public isReady: boolean = false;

  constructor(private application: Application) {}

  public ngOnInit(): void {
    this.application.start().subscribe((x) => (this.isReady = true));
  }
}
