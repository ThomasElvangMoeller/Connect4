import { Injectable } from "@angular/core";
import signalR from "@microsoft/signalr";
import { User, UserService } from "./user.service";

@Injectable({
   providedIn: "root",
})
export class GameService {
   private gameConnection = new signalR.HubConnectionBuilder().withUrl("/gamehub").build();
   public gameId: string;
   public lobby: Lobby;

   constructor(private userService: UserService) {
      this.gameConnection.on("SendLobby", (lobby: Lobby) => {
         this.lobby = lobby;
      });
   }

   public createLobby(password: string | null) {
      this.gameConnection.invoke<Lobby>("SendLobby", this.userService.getUser().name, password).then((val) => {
         this.lobby = val;
      });
      this.gameConnection;
   }
}

export interface Lobby {
   name: string;
   id: string;
   players: User[];
}
