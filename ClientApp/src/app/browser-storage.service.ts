import { Inject, Injectable, InjectionToken } from "@angular/core";

export const BROWSER_STORAGE = new InjectionToken<Storage>("Browser Storage", {
   providedIn: "root",
   factory: () => localStorage,
});

@Injectable({
   providedIn: "root",
})
export class BrowserStorageService {
   constructor(@Inject(BROWSER_STORAGE) public storage: Storage) {}

   get(key: string) {
      return this.storage.getItem(key);
   }

   public getObject<T>(key: string): T | null {
      const item = this.storage.getItem(key);
      if (item) return JSON.parse(item) as T;
      return null;
   }

   public setObject<T>(key: string, item: T) {
      this.storage.setItem(key, JSON.stringify(item));
   }

   set(key: string, value: string) {
      this.storage.setItem(key, value);
   }

   remove(key: string) {
      this.storage.removeItem(key);
   }

   clear() {
      this.storage.clear();
   }
}
