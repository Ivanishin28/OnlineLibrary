import { DateOnly } from '../../types/dateOnly';
import { AuthorPreview } from './apiModels/authorPreview';

export interface FullBook {
  id: string;
  title: string;
  cover_id: string | undefined;
  authors: AuthorPreview[];
  description: string | undefined;
  publishing_date: DateOnly;
}
