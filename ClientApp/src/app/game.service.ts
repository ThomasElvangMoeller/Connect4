import { Injectable } from "@angular/core";
import signalR from "@microsoft/signalr";
import { Game } from "src/models/game";
import { Lobby } from "src/models/lobby";
import { UserService } from "./user.service";

@Injectable({
   providedIn: "root",
})
export class GameService {
   private gameConnection: signalR.HubConnection;
   public gameId: string;
   public lobby?: Lobby;
   public game?: Game;
   public error?: string;

   constructor(private userService: UserService) {
      this.createConnection();
      this.gameConnection.on("SendLobby", (lobby: Lobby) => {
         this.lobby = lobby;
      });
      this.gameConnection.on("UpdateSettingsResponse", (success: boolean, response: Lobby | string) => {
         if (success) {
            this.lobby = response as Lobby;
         } else {
            this.error = response as string;
         }
      });
      this.gameConnection.on("StartGame", (success: boolean, response: Game | string) => {
         if (success) {
            this.lobby = null;
            this.game = response as Game;
         } else {
            this.error = response as string;
         }
      });
   }

   public createConnection(): void {
      if (!this.gameConnection) this.gameConnection = new signalR.HubConnectionBuilder().withUrl("/gamehub").build();
   }

   public createLobby(password: string | null) {
      this.gameConnection.invoke<Lobby>("CreateLobby", this.userService.getUser().name, password).then((val: Lobby) => {
         this.lobby = val;
      });
      this.gameConnection;
   }
}
