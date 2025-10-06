import { Tag } from './tag';

export interface ShelvedBook {
  id: string;
  shelf_id: string;
  book_id: string;
  date_shelved: Date;

  tags: Tag[];
}
