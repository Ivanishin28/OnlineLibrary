import { BookTag } from './bookTag';

export interface ShelvedBook {
  id: string;
  shelf_id: string;
  book_id: string;
  date_shelved: Date;

  tags: BookTag[];
}
