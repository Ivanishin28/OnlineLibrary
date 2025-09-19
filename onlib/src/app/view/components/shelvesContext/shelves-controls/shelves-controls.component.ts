import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { ShelfService } from '../../../../business/services/shelves/shelf.service';
import { switchMap } from 'rxjs';
import { ShelfPreview } from '../../../../business/models/bookshelves/shelfPreview';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'shelves-controls',
  imports: [CommonModule],
  templateUrl: './shelves-controls.component.html',
  styleUrl: './shelves-controls.component.scss',
  providers: [ShelfService],
})
export class ShelvesControlsComponent implements OnInit {
  public shelves: ShelfPreview[] = [];

  constructor(
    private authService: AuthService,
    private shelfService: ShelfService
  ) {}

  public ngOnInit(): void {
    this.authService.loggedUserId$
      .pipe(switchMap((userId) => this.shelfService.getByUserId(userId)))
      .subscribe((shelves) => (this.shelves = shelves));
  }
}
