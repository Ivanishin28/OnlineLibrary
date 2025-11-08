import { DateOnly } from '../../types/dateOnly';
import { Genre } from '../shelves/genre';
import { FullAuthor } from './fullAuthor';

export interface FullBook {
  id: string;
  title: string;
  cover_id: string | undefined;
  file_id: string | undefined;
  authors: FullAuthor[];
  description: string | undefined;
  publishing_date: DateOnly;
  genres: Genre[];
}
