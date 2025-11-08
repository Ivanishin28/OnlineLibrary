import { CommonModule } from '@angular/common';
import {
  Component,
  ContentChild,
  Input,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BookCoverComponent } from '../book-cover/book-cover.component';
import { BookPreview } from '../../../../business/models/books/bookPreview';
import { RouterModule } from '@angular/router';
import { Popover } from 'primeng/popover';
import { BookInfoPanelComponent } from '../book-card-with-popover/book-info-panel/book-info-panel.component';

@Component({
  standalone: true,
  selector: 'book-card',
  imports: [
    CommonModule,
    BookCoverComponent,
    RouterModule,
    Popover,
    BookInfoPanelComponent,
  ],
  templateUrl: './book-card.component.html',
  styleUrl: './book-card.component.scss',
})
export class BookCardComponent {
  @Input({ required: true }) book!: BookPreview;
  @Input() navigateOnImageClick: boolean = false;
  @Input() showPopover: boolean = true;

  @ContentChild('bottom') bottomTemplate?: TemplateRef<any>;

  @ViewChild('popover') overlayPanel?: any;

  public isTargetHovered = false;
  public isPopoverHovered = false;

  public onTargetEnter(event: MouseEvent): void {
    if (!this.showPopover || !this.overlayPanel) {
      return;
    }
    this.isTargetHovered = true;
    this.overlayPanel.show(event);
  }

  public onTargetLeave(event: MouseEvent): void {
    if (!this.showPopover || !this.overlayPanel) {
      return;
    }
    this.isTargetHovered = false;
    this.checkClose();
  }

  public onPopoverEnter(): void {
    this.isPopoverHovered = true;
  }

  public onPopoverLeave(): void {
    this.isPopoverHovered = false;
    this.checkClose();
  }

  private checkClose(): void {
    if (!this.overlayPanel) {
      return;
    }
    setTimeout(() => {
      if (!this.isTargetHovered && !this.isPopoverHovered) {
        this.overlayPanel.hide();
      }
    }, 100);
  }
}
