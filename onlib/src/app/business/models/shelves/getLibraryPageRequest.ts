import { Page } from '../_shared/page';
import { LibraryFilter } from './libraryFilter';

export interface GetLibraryPageRequest {
  filter: LibraryFilter;
  user_id: string;
  page: Page;
}
