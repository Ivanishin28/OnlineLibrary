export interface CreateAuthorRequest {
  first_name: string;
  last_name: string;
  birth_date: Date;
  avatar_id: string | undefined;
  biography: string | undefined;
}
