export interface RegisterRequest {
  login: string;
  email: string;
  password: string;
  avatar_id: string | undefined;

  first_name: string;
  last_name: string;
  birth_date: Date;
}
