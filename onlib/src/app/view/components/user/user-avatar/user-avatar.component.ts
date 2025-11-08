import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';
import { MediaImageComponent } from '../../_shared/media-image/media-image.component';

@Component({
  selector: 'user-avatar',
  imports: [MediaImageComponent],
  templateUrl: './user-avatar.component.html',
  styleUrl: './user-avatar.component.scss',
})
export class UserAvatarComponent implements OnChanges {
  @Input({ required: true }) fileId: string | undefined;

  public avatar!: MediaFileId | undefined;

  public ngOnChanges(): void {
    this.avatar = this.fileId ? new MediaFileId(this.fileId) : undefined;
  }
}
