import { StorageKey } from '../models/_shared/storageKey';

export class AuthStorageKeys {
  public static readonly identityId = new StorageKey<string>('identity_id');
}
