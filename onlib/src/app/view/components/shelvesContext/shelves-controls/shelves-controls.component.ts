import { Component, Input, OnInit } from '@angular/core';
import { PersonalShelfService } from '../../../../business/services/shelves/personalShelf.service';
import { ShelfPreview } from '../../../../business/models/shelves/shelfPreview';
import { CommonModule } from '@angular/common';
import { ShelfCreationWindowManager } from '../../../../business/managers/windows/shelfCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { UserId } from '../../../../business/models/_shared/userId';
import { map, Observable, pipe, switchMap, take, tap } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { ShelfControlComponent } from './shelf-control/shelf-control.component';
import { AuthService } from '../../../../business/services/auth/auth.service';

@Component({
  standalone: true,
  selector: 'shelves-controls',
  imports: [
    CommonModule,
    DynamicDialogModule,
    ButtonModule,
    ShelfControlComponent,
  ],
  providers: [ShelfCreationWindowManager],
  templateUrl: './shelves-controls.component.html',
  styleUrl: './shelves-controls.component.scss',
})
export class ShelvesControlsComponent implements OnInit {
  private userId!: UserId;

  public shelves: ShelfPreview[] = [];

  constructor(
    private shelfService: PersonalShelfService,
    private creationWindow: ShelfCreationWindowManager,
    private authService: AuthService
  ) {}

  public ngOnInit(): void {
    this.authService.loggedUser$
      .pipe(
        take(1),
        pipe(
          tap((x) => (this.userId = x.userId)),
          switchMap((x) => this.loadShelves())
        )
      )
      .subscribe();
  }

  public createShelf(): void {
    this.creationWindow
      .createShelfFor(this.userId)
      .pipe(switchMap(() => this.loadShelves()))
      .subscribe();
  }

  public delete(shelf: ShelfPreview): void {
    this.shelfService
      .delete(shelf.id)
      .pipe(switchMap((x) => this.loadShelves()))
      .subscribe();
  }

  public rename(shelfId: string, name: string): void {
    this.shelfService.rename(shelfId, name).subscribe();
  }

  private loadShelves(): Observable<void> {
    return this.shelfService.getByUserId(this.userId).pipe(
      tap((shelves) => (this.shelves = shelves)),
      map((x) => void 0)
    );
  }
}
