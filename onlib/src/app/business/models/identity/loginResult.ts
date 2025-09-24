import { TokenDto } from './tokenDto';

export interface LoginResult {
  identity_id: string;
  user_id: string;
  login: string;
  email: string;

  token: TokenDto;
}
