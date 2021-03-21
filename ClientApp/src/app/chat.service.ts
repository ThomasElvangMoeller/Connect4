import { Injectable, SecurityContext } from "@angular/core";
import { DomSanitizer } from "@angular/platform-browser";
import * as signalR from "@microsoft/signalr";
import { Subject } from "rxjs";

@Injectable({
   providedIn: "root",
})
export class ChatService {
   private chatConnection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
   public chatLog: Subject<ChatLog>;
   public gameId: string;

   constructor(private soap: DomSanitizer) {
      this.chatLog = new Subject();

      this.chatConnection.on("recieveMessage", ({ sender, message }) => {
         this.chatLog.next({
            timestamp: new Date(),
            author: sender,
            message: soap.sanitize(SecurityContext.HTML, message),
         });
      });

      //always put last
      this.startConnection();
   }

   public SendMessage(message: string): Promise<void> {
      //console.log("service called. message: " + message + " gameId: " + this.gameId);
      if (!this.gameId) {
         return Promise.reject("No gameId, cannot connect to game group");
      }
      return this.chatConnection.send("SendMessage", this.gameId, message);
   }

   public joinGroup(): Promise<void> {
      if (this.gameId) {
         return this.chatConnection.send("AddToGroup", this.gameId);
      }
      return Promise.reject("No gameId, cannot create connection to game group");
   }

   private callbacks: ((chat: ChatService) => void)[] = [];
   public onConnected(callback: (chat: ChatService) => void) {
      // This is a mess, refactor this
      this.callbacks.push(callback);
   }

   private fireOnConnected() {
      this.callbacks.forEach((e) => e(this));
   }

   private startConnection() {
      this.chatConnection
         .start()
         .then(() => this.fireOnConnected())
         .catch((err) => {
            console.warn(err);
            setTimeout(() => {
               this.startConnection();
            }, 5000);
         });
   }
}

export interface ChatLog {
   timestamp: Date;
   author: string;
   message: string;
}
