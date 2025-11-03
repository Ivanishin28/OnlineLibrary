import { Component, OnInit } from '@angular/core';
import { TagService } from '../../../../business/services/shelves/tag.service';
import { CommonModule } from '@angular/common';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { TagCreationWindowManager } from '../../../../business/managers/windows/tagCreationWindowManager';
import { Tag } from '../../../../business/models/shelves/tag';
import { ButtonModule } from 'primeng/button';
import { TagControlComponent } from './tag-control/tag-control.component';

@Component({
  standalone: true,
  selector: 'tags-controls',
  imports: [
    CommonModule,
    DynamicDialogModule,
    ButtonModule,
    TagControlComponent,
  ],
  providers: [TagCreationWindowManager],
  templateUrl: './tags-controls.component.html',
  styleUrl: './tags-controls.component.scss',
})
export class TagsControlsComponent implements OnInit {
  public tags: Tag[] | undefined;

  constructor(
    private tagService: TagService,
    private tagCreationWindowManager: TagCreationWindowManager
  ) {}

  public ngOnInit(): void {
    this.loadTags();
  }

  public createTag(): void {
    this.tagCreationWindowManager.createTag().subscribe(() => this.loadTags());
  }

  public delete(tag: Tag): void {
    this.tagService.delete(tag.id).subscribe(() => this.loadTags());
  }

  public onTagNameChange(tag: Tag, newName: string): void {
    this.tagService.rename(tag.id, newName).subscribe((x) => {});
  }

  private loadTags(): void {
    this.tagService.getPersonalTags().subscribe((x) => (this.tags = x));
  }
}
