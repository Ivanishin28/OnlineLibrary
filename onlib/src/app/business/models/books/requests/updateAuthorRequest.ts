export interface UpdateAuthorRequest {
  id: string;
  birth_date: Date;
  avatar_id: string;
  biography: string | undefined;
}
