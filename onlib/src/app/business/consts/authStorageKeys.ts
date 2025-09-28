import { StorageKey } from '../models/_shared/storageKey';
import { Token } from '../models/_shared/token';
import { UserCredentials } from '../models/_shared/userCredentials';

export class AuthStorageKeys {
  public static readonly USER_CREDENTIALS = new StorageKey<UserCredentials>(
    'user_credentials'
  );
  public static readonly TOKEN = new StorageKey<Token>('token');
}
