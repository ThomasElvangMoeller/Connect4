import { User } from "./user";

export interface Lobby {
   name: string;
   id: string;
   players: User[];
}
