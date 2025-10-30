import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MediaImageComponent } from '../../_shared/media-image/media-image.component';
import { MediaFileId } from '../../../../business/models/_shared/mediaFileId';

@Component({
  selector: 'book-cover',
  imports: [MediaImageComponent],
  templateUrl: './book-cover.component.html',
  styleUrl: './book-cover.component.scss',
})
export class BookCoverComponent implements OnChanges {
  @Input({ required: true }) coverId: string | undefined;

  public fileId: MediaFileId | undefined;

  public ngOnChanges(): void {
    this.fileId = !!this.coverId ? new MediaFileId(this.coverId) : undefined;
  }
}
