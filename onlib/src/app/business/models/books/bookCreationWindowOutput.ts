export class BookCreationWindowOutput {
  constructor(
    public readonly title: string,
    public readonly publishing_date: Date,
    public readonly author_ids_input: string | null,
    public readonly description: string | null,
    public readonly cover_id: string | null
  ) {}
}


