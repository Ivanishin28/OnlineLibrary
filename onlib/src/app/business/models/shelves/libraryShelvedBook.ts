import { BookPreview } from '../books/bookPreview';
import { ShelvedBook } from './shelvedBook';

export interface LibraryShelvedBook extends ShelvedBook {
  book: BookPreview;
}
