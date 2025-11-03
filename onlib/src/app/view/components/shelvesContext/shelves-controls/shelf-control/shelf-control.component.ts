import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ShelfPreview } from '../../../../../business/models/shelves/shelfPreview';
import { ShelfService } from '../../../../../business/services/shelves/shelf.service';

@Component({
  standalone: true,
  selector: 'shelf-control',
  imports: [CommonModule, FormsModule, ButtonModule, InputTextModule],
  templateUrl: './shelf-control.component.html',
  styleUrl: './shelf-control.component.scss',
})
export class ShelfControlComponent implements OnChanges {
  @Input({ required: true }) shelf!: ShelfPreview;

  @Output() changeName: EventEmitter<string> = new EventEmitter<string>();
  @Output() delete: EventEmitter<ShelfPreview> =
    new EventEmitter<ShelfPreview>();

  public isEditing: boolean = false;
  public editedName: string = '';

  constructor(private shelfService: ShelfService) {}

  public ngOnChanges(): void {
    this.editedName = this.shelf.name;
  }

  public enableEditing(): void {
    this.isEditing = true;
  }

  public saveName(): void {
    if (!this.isValid) {
      return;
    }

    this.shelfService
      .isNameTaken(this.editedName)
      .subscribe((isTaken) => {
        if (!isTaken) {
          this.changeName.emit(this.editedName.trim());
          this.isEditing = false;
        }
      });
  }

  public get isValid(): boolean {
    return !!this.editedName && this.editedName !== this.shelf.name;
  }

  public cancelEditing(): void {
    this.isEditing = false;
    this.editedName = this.shelf.name;
  }

  public onDelete(): void {
    this.delete.emit(this.shelf);
  }
}
