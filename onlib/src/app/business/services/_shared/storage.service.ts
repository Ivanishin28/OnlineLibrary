import { Injectable } from '@angular/core';
import { StorageKey } from '../../models/_shared/storageKey';

@Injectable()
export class StorageService {
  public set<T>(key: StorageKey<T>, value: T): void {
    const json = JSON.stringify(value);
    localStorage.setItem(key.label, json);
  }

  public get<T>(key: StorageKey<T>): T | undefined {
    const item = localStorage.getItem(key.label);
    if (!item) {
      return undefined;
    }

    return JSON.parse(item);
  }

  public remove(key: StorageKey<any>): void {
    localStorage.removeItem(key.label);
  }

  public clear(): void {
    localStorage.clear();
  }
}
