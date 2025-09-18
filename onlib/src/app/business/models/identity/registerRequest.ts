export interface RegisterRequest {
  login: string;
  email: string;
  password: string;

  first_name: string;
  last_name: string;
  birth_date: Date;
}
