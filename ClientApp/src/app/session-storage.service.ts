import { Inject, Injectable, InjectionToken } from "@angular/core";

export const SESSION_STORAGE = new InjectionToken<Storage>("Session Storage", {
   providedIn: "root",
   factory: () => sessionStorage,
});

@Injectable({
   providedIn: "root",
})
export class SessionStorageService {
   constructor(@Inject(SESSION_STORAGE) public storage: Storage) {}

   get(key: string) {
      return this.storage.getItem(key);
   }

   getObject<T>(key: string) {
      const item = this.storage.getItem(key);
      if (item) return JSON.parse(item) as T;
      return null;
   }

   setObject<T>(key: string, item: T) {
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
