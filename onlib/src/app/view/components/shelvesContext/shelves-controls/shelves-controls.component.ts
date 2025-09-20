import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../../business/services/auth/auth.service';
import { ShelfService } from '../../../../business/services/shelves/shelf.service';
import { switchMap, tap } from 'rxjs';
import { ShelfPreview } from '../../../../business/models/shelves/shelfPreview';
import { CommonModule } from '@angular/common';
import { ShelfCreationWindowManager } from '../../../../business/managers/windows/shelfCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';

@Component({
  standalone: true,
  selector: 'shelves-controls',
  imports: [CommonModule, DynamicDialogModule],
  providers: [ShelfService, ShelfCreationWindowManager],
  templateUrl: './shelves-controls.component.html',
  styleUrl: './shelves-controls.component.scss',
})
export class ShelvesControlsComponent implements OnInit {
  public shelves: ShelfPreview[] = [];

  public userId!: string;

  constructor(
    private authService: AuthService,
    private shelfService: ShelfService,
    private creationWindow: ShelfCreationWindowManager
  ) {}

  public ngOnInit(): void {
    this.authService.loggedUserId$
      .pipe(
        tap((user_id) => (this.userId = user_id)),
        switchMap((userId) => this.shelfService.getByUserId(userId))
      )
      .subscribe((shelves) => (this.shelves = shelves));
  }

  public createShelf(): void {
    this.creationWindow
      .createShelfFor(this.userId)
      .subscribe((x) => console.log(x));
  }
}
