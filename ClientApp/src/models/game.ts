export interface Game {
   Id: string;
   gameBoard: BoardTile[][];
   cardDrawPile: number[];
   cardDiscardPile: number[];
   currentPlayerTurnIndex: number;
}

export interface BoardTile {
   tileValue: number;
   playerPresence: Map<string, number>;
}

export interface PlayerGameState {
   player: string;
   playerPieces: number;
   playerColor: PlayerColor;
   cards: number[];
}

export enum PlayerColor {
   White,
   Black,
   Grey,
   Red,
}
