import { Observable, Subject } from 'rxjs';
import { Page } from './page';

export class Paginator {
  private _pagination: Subject<Page> = new Subject<Page>();

  constructor(private page: Page) {}

  public readonly paginationChanged$: Observable<Page> =
    this._pagination.asObservable();

  public get pageSize(): number {
    return this.page.pageSize;
  }

  public get pageIndex(): number {
    return this.page.pageIndex;
  }

  public loadFirstPage(): void {
    this.loadPagination({ pageIndex: 0, pageSize: this.page.pageSize });
  }

  public reloadPage(): void {
    this.loadPagination(this.page);
  }

  public loadPagination(page: Page): void {
    this.page = page;
    this._pagination.next(page);
  }
}
