import { Component, Input, ViewChild } from '@angular/core';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { BookCardComponent } from '../book-card/book-card.component';
import { Popover } from 'primeng/popover';
import { BookInfoPanelComponent } from './book-info-panel/book-info-panel.component';

@Component({
  standalone: true,
  selector: 'book-card-with-popover',
  imports: [BookCardComponent, Popover, BookInfoPanelComponent],
  templateUrl: './book-card-with-popover.component.html',
  styleUrl: './book-card-with-popover.component.scss',
})
export class BookCardWithPopoverComponent {
  @Input({ required: true }) book!: BookPreview;

  @ViewChild('popover') overlayPanel!: any;
  
  public isTargetHovered = false;
  public isPopoverHovered = false;

  public onTargetEnter(event: MouseEvent) {
    this.isTargetHovered = true;
    this.overlayPanel.show(event);
  }

  public onTargetLeave(event: MouseEvent) {
    this.isTargetHovered = false;
    this.checkClose();
  }

  public onPopoverEnter() {
    this.isPopoverHovered = true;
  }

  public onPopoverLeave() {
    this.isPopoverHovered = false;
    this.checkClose();
  }

  private checkClose() {
    setTimeout(() => {
      if (!this.isTargetHovered && !this.isPopoverHovered) {
        this.overlayPanel.hide();
      }
    }, 100);
  }
}
