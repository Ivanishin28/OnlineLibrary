import { Observable, Subject } from 'rxjs';
import { Page } from './page';

export class Paginator {
  private _pagination: Subject<Page> = new Subject<Page>();
  private _page: Page;

  constructor(page: Page) {
    this._page = page;
  }

  public readonly paginationChanged$: Observable<Page> =
    this._pagination.asObservable();

  public get page(): Page {
    return this._page;
  }

  public get pageSize(): number {
    return this.page.page_size;
  }

  public get pageIndex(): number {
    return this.page.page_index;
  }

  public loadFirstPage(): void {
    this.loadPagination({ page_index: 0, page_size: this.page.page_size });
  }

  public reloadPage(): void {
    this.loadPagination(this.page);
  }

  public loadPagination(page: Page): void {
    this._page = page;
    this._pagination.next(page);
  }
}
