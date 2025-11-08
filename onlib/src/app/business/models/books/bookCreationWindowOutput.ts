import { Genre } from '../shelves/genre';
import { AuthorPreview } from './apiModels/authorPreview';

export class BookCreationWindowOutput {
  constructor(
    public readonly title: string,
    public readonly publishing_date: Date,
    public readonly selectedAuthors: AuthorPreview[],
    public readonly description: string | null,
    public readonly cover_id: string | null,
    public readonly selectedGenres: Genre[] = [],
    public readonly file_id: string | null = null
  ) {}
}
