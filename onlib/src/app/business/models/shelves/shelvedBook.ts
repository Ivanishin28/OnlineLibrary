import { BookTag } from './bookTag';
import { ShelfPreview } from './shelfPreview';

export interface ShelvedBook {
  id: string;
  book_id: string;
  date_shelved: Date;

  shelf: ShelfPreview;
  tags: BookTag[];
}
