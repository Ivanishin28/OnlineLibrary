import { ShelfSummary } from './shelfSummary';
import { TagSummary } from './tagSummary';

export interface LibrarySummary {
  user_id: string;
  shelves: ShelfSummary[];
  tags: TagSummary[];
  book_count: number;
}
