export interface CreateBookRequest {
  title: string;
  author_ids: string[];
  publishing_date: Date;
  cover_id?: string | null;
  description?: string | null;
}
