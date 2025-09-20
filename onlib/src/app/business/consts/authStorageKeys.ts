import { StorageKey } from '../models/_shared/storageKey';
import { UserCredentials } from '../models/_shared/userCredentials';

export class AuthStorageKeys {
  public static readonly USER_CREDENTIALS = new StorageKey<UserCredentials>(
    'user_credentials'
  );
}
