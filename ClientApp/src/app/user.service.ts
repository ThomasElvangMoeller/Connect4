import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { BrowserStorageService } from "./browser-storage.service";
import { SessionStorageService } from "./session-storage.service";

@Injectable({
   providedIn: "root",
})
export class UserService {
   // Store in localStorage
   private userName: string | null;

   // Store in SessionStorage
   private session: userSession;

   constructor(
      private browserStorage: BrowserStorageService,
      private sessionStorage: SessionStorageService,
      private http: HttpClient,
      @Inject("BASE_URL") private baseUrl: string
   ) {
      const usr = this.getUserFromStorage();
      if (usr.name) {
         this.userName = usr.name;
      }
   }

   public async validate(name: string): Promise<{ valid: boolean; message: string | null }> {
      const url = this.baseUrl + "api/player/validateplayername";
      return this.http
         .post(url, { playerName: name })
         .toPromise()
         .then(() => {
            return { valid: true, message: null };
         })
         .catch((reason) => {
            return { valid: false, message: reason };
         });
   }

   public getUser(): User | null {
      return {
         name: this.userName,
         applicationUserGuid: this.session.applicationUserGuid,
         connectionId: this.session.connectionId,
      };
   }

   public setUser(user: User) {
      if (user.name) this.userName = user.name;
      if (user.connectionId)
         this.session = { applicationUserGuid: user.applicationUserGuid, connectionId: user.connectionId };
      this.browserStorage.set(USER_STORAGE_TOKEN, user.name);
      this.sessionStorage.setObject<userSession>(USER_STORAGE_TOKEN, this.session);
   }

   public getUserFromStorage(): User {
      const name = this.browserStorage.get(USER_STORAGE_TOKEN);
      const sess = this.sessionStorage.getObject<userSession>(USER_STORAGE_TOKEN);
      return { name, connectionId: sess.connectionId, applicationUserGuid: sess.applicationUserGuid };
   }
}

//might want to rename this
export interface User {
   name: string;
   connectionId: string;
   applicationUserGuid: string | null;
}

interface userSession {
   connectionId: string | null;
   applicationUserGuid: string | null;
}

const USER_STORAGE_TOKEN = "user";
