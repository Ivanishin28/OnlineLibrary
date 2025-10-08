import { Component, Input, OnInit } from '@angular/core';
import { ShelfService } from '../../../../business/services/shelves/shelf.service';
import { ShelfPreview } from '../../../../business/models/shelves/shelfPreview';
import { CommonModule } from '@angular/common';
import { ShelfCreationWindowManager } from '../../../../business/managers/windows/shelfCreationWindowManager';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { UserId } from '../../../../business/models/_shared/userId';

@Component({
  standalone: true,
  selector: 'shelves-controls',
  imports: [CommonModule, DynamicDialogModule],
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
    this.shelfService
      .getByUserId(this.userId)
      .subscribe((shelves) => (this.shelves = shelves));
  }

  public createShelf(): void {
    this.creationWindow.createShelfFor(this.userId).subscribe();
  }
}
