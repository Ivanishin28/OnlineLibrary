import { Component, Input, OnInit } from '@angular/core';
import { ShelfService } from '../../../../business/services/shelves/shelf.service';
import { ShelfPreview } from '../../../../business/models/shelves/shelfPreview';
import { CommonModule } from '@angular/common';
import { ShelfCreationWindowManager } from '../../../../business/managers/windows/shelfCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { UserId } from '../../../../business/models/_shared/userId';
import { Shelf } from '../../../../business/models/shelves/shelf';
import { map, Observable, switchMap, tap } from 'rxjs';
import { ButtonModule } from 'primeng/button';

@Component({
  standalone: true,
  selector: 'shelves-controls',
  imports: [CommonModule, DynamicDialogModule, ButtonModule],
  providers: [ShelfCreationWindowManager],
  templateUrl: './shelves-controls.component.html',
  styleUrl: './shelves-controls.component.scss',
})
export class ShelvesControlsComponent implements OnInit {
  @Input({ required: true }) userId!: UserId;

  public shelves: ShelfPreview[] = [];

  constructor(
    private shelfService: ShelfService,
    private creationWindow: ShelfCreationWindowManager
  ) {}

  public ngOnInit(): void {
    this.loadShelves().subscribe();
  }

  public createShelf(): void {
    this.creationWindow
      .createShelfFor(this.userId)
      .pipe(switchMap(() => this.loadShelves()))
      .subscribe();
  }

  public delete(shelf: Shelf): void {
    this.shelfService
      .delete(shelf.id)
      .pipe(switchMap((x) => this.loadShelves()))
      .subscribe();
  }

  private loadShelves(): Observable<void> {
    return this.shelfService.getByUserId(this.userId).pipe(
      tap((shelves) => (this.shelves = shelves)),
      map((x) => void 0)
    );
  }
}
