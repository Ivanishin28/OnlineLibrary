import { Component, Input, OnInit } from '@angular/core';
import { UserId } from '../../../../business/models/_shared/userId';
import { TagService } from '../../../../business/services/shelves/tag.service';
import { CommonModule } from '@angular/common';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { TagCreationWindowManager } from '../../../../business/managers/windows/tagCreationWindowManager';

@Component({
  standalone: true,
  selector: 'tags-controls',
  imports: [CommonModule, DynamicDialogModule],
  providers: [TagCreationWindowManager],
  templateUrl: './tags-controls.component.html',
  styleUrl: './tags-controls.component.scss',
})
export class TagsControlsComponent {
  @Input({ required: true }) userId!: UserId;

  constructor(
    private tagService: TagService,
    private tagCreationWindowManager: TagCreationWindowManager
  ) {}

  public createTag(): void {
    this.tagCreationWindowManager.createTag().subscribe((x) => console.log(x));
  }
}
