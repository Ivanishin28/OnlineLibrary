import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Page } from '../../../../business/models/_shared/page';
import { PaginatorModule, PaginatorState } from 'primeng/paginator';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'pagination',
  imports: [CommonModule, PaginatorModule],
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.scss',
})
export class PaginationComponent implements OnChanges {
  @Input({ required: true }) pageIndex!: number;
  @Input({ required: true }) pageSize!: number;
  @Input({ required: true }) totalItems!: number;

  @Output() paginationChange: EventEmitter<Page> = new EventEmitter<Page>();

  public start = 0;

  public ngOnChanges(): void {
    this.start = this.pageIndex * this.pageSize;
  }

  public onPageChange(state: PaginatorState) {
    this.paginationChange.next({
      page_index: state.page!,
      page_size: state.rows!,
    });
  }
}
