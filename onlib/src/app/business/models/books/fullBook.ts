import { DateOnly } from '../../types/dateOnly';
import { FullAuthor } from './fullAuthor';

export interface FullBook {
  id: string;
  title: string;
  cover_id: string | undefined;
  authors: FullAuthor[];
  description: string | undefined;
  publishing_date: DateOnly;
}
