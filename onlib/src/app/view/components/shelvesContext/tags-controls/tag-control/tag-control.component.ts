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
import { Tag } from '../../../../../business/models/shelves/tag';
import { TagService } from '../../../../../business/services/shelves/tag.service';
import { MessageWindowManager } from '../../../../../business/managers/windows/messageWindowManager';
import { AuthService } from '../../../../../business/services/auth/auth.service';
import { take } from 'rxjs';

@Component({
  standalone: true,
  selector: 'tag-control',
  imports: [CommonModule, FormsModule, ButtonModule, InputTextModule],
  providers: [MessageWindowManager],
  templateUrl: './tag-control.component.html',
  styleUrl: './tag-control.component.scss',
})
export class TagControlComponent implements OnChanges {
  @Input({ required: true }) tag!: Tag;

  @Output() changeName: EventEmitter<string> = new EventEmitter<string>();
  @Output() delete: EventEmitter<Tag> = new EventEmitter<Tag>();

  public isEditing: boolean = false;
  public editedName: string = '';

  constructor(
    private tagService: TagService,
    private messageWindowManager: MessageWindowManager,
    private authService: AuthService
  ) {}

  public ngOnChanges(): void {
    this.editedName = this.tag.name;
  }

  public enableEditing(): void {
    this.isEditing = true;
  }

  public saveName(): void {
    if (!this.isValid) {
      return;
    }

    this.authService.loggedUser$.pipe(take(1)).subscribe((user) => {
      this.tagService
        .isTagNameTakenByUser(user.userId, this.editedName)
        .subscribe((isTaken) => {
          if (!isTaken) {
            this.changeName.emit(this.editedName.trim());
            this.isEditing = false;
          } else {
            this.messageWindowManager.show('Error', 'This name is taken');
          }
        });
    });
  }

  public get isValid(): boolean {
    return !!this.editedName && this.editedName !== this.tag.name;
  }

  public cancelEditing(): void {
    this.isEditing = false;
    this.editedName = this.tag.name;
  }

  public onDelete(): void {
    this.delete.emit(this.tag);
  }
}

