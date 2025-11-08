import { Genre } from '../shelves/genre';

export interface UpdateBookRequest {
  id: string;
  author_ids: string[];
  publishing_date: Date;
  cover_id?: string | null;
  file_id?: string | null;
  description?: string | null;
  genre_ids: string[];
}
