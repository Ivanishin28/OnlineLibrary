import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
  ViewChild,
  ElementRef,
  AfterViewChecked,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { ShelfPreview } from '../../../../../business/models/shelves/shelfPreview';
import { PersonalShelfService } from '../../../../../business/services/shelves/personalShelf.service';
import { MessageWindowManager } from '../../../../../business/managers/windows/messageWindowManager';

@Component({
  standalone: true,
  selector: 'shelf-control',
  imports: [CommonModule, FormsModule, ButtonModule, InputTextModule],
  providers: [MessageWindowManager],
  templateUrl: './shelf-control.component.html',
  styleUrl: './shelf-control.component.scss',
})
export class ShelfControlComponent implements OnChanges, AfterViewChecked {
  @Input({ required: true }) shelf!: ShelfPreview;
  @ViewChild('nameInput') nameInputRef!: ElementRef<HTMLInputElement>;

  @Output() changeName: EventEmitter<string> = new EventEmitter<string>();
  @Output() delete: EventEmitter<ShelfPreview> =
    new EventEmitter<ShelfPreview>();

  public isEditing: boolean = false;
  public editedName: string = '';
  private shouldFocus: boolean = false;

  constructor(
    private shelfService: PersonalShelfService,
    private messageWindowManager: MessageWindowManager
  ) {}

  public ngOnChanges(): void {
    this.editedName = this.shelf.name;
  }

  public enableEditing(): void {
    this.isEditing = true;
    this.shouldFocus = true;
  }

  public ngAfterViewChecked(): void {
    if (this.shouldFocus && this.nameInputRef?.nativeElement) {
      this.nameInputRef.nativeElement.focus();
      this.nameInputRef.nativeElement.select();
      this.shouldFocus = false;
    }
  }

  public saveName(): void {
    if (!this.isValid) {
      return;
    }

    this.shelfService.isNameTaken(this.editedName).subscribe((isTaken) => {
      if (!isTaken) {
        this.changeName.emit(this.editedName.trim());
        this.isEditing = false;
      } else {
        this.messageWindowManager.show('Error', 'This name is taken');
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
