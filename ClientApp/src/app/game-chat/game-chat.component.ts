import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Subscription } from "rxjs";
import { ChatLog, ChatService } from "../chat.service";

@Component({
   selector: "app-game-chat",
   templateUrl: "./game-chat.component.html",
   styleUrls: ["./game-chat.component.scss"],
})
export class GameChatComponent implements OnInit {
   public chatLog: ChatLog[] = [];

   public chatForm = new FormGroup({
      message: new FormControl(""),
   });
   chatLogSubscription: Subscription; // Is this neccesary to save?

   constructor(private chat: ChatService) {}

   ngOnInit() {
      this.chatLogSubscription = this.chat.chatLog.subscribe((message) => {
         this.chatLog.push(message);
      });
      this.chat.gameId = "test";
      this.chat.onConnected((chat) => {
         chat.joinGroup().then(() => console.log("joined group"));
      });
   }

   public send() {
      this.chat.SendMessage(this.chatForm.controls["message"].value);
      this.chatForm.setValue({ message: "" });
   }
}
