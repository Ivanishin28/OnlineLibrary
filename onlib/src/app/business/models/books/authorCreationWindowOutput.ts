export class AuthorCreationWindowOutput {
  constructor(
    public readonly first_name: string,
    public readonly last_name: string,
    public readonly birth_date: Date,
    public readonly authorId?: string,
    public readonly avatarId?: string,
    public readonly biography?: string
  ) {}
}
