import { StorageKey } from '../models/_shared/storageKey';

export class AuthStorageKeys {
  public static readonly USER_ID = new StorageKey<string>('user_id');
}
