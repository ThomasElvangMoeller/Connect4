import { Injectable } from "@angular/core";
import { BrowserStorageService } from "./browser-storage.service";
import { SessionStorageService } from "./session-storage.service";

@Injectable({
   providedIn: "root",
})
export class UserService {
   private user: User | null;
   constructor(private browserStorage: BrowserStorageService, private sessionStorage: SessionStorageService) {
      const usr = this.getUserFromStorage();
      if (usr) {
         this.user = usr;
      }
   }

   public getUser(): User | null {
      return this.user;
   }

   public setUser(user: User) {
      this.user = user;
      this.browserStorage.setObject<User>(USER_STORAGE_TOKEN, user);
   }

   public getUserFromStorage(): User | null {
      return this.browserStorage.getObject<User>(USER_STORAGE_TOKEN);
   }
}

export interface User {
   name: string;
   token: string;
}

const USER_STORAGE_TOKEN = "user";
