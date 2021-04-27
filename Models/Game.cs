using Connect4.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Connect4.Models
{
    /// <summary>
    /// Created after the lobby. After the "host" presses start, the game is created with the given players.
    /// Game is currently missing functionality for removing players midgame.
    /// Adding players midgame will not be supported
    /// </summary>
    public class Game
    {
        public Guid Id { get; set; }

        public PlayerGameState[] Players { get; set; } = new PlayerGameState[4];
        public PlayerGameState PlayerWhite
        {
            get => Players[(int)PlayerColor.White];
            set => Players[(int)PlayerColor.White] = value;
        }
        public PlayerGameState PlayerBlack
        {
            get => Players[(int)PlayerColor.Black];
            set => Players[(int)PlayerColor.Black] = value;
        }
        public PlayerGameState PlayerGrey
        {
            get => Players[(int)PlayerColor.Grey];
            set => Players[(int)PlayerColor.Grey] = value;
        }
        public PlayerGameState PlayerRed
        {
            get => Players[(int)PlayerColor.Red];
            set => Players[(int)PlayerColor.Red] = value;
        }
        public BoardTile[,] GameBoard { get; set; }
        public Stack<int> CardDrawPile { get; set; }
        public Stack<int> CardDiscardPile { get; set; }
        public int CurrentPlayerTurnIndex { get; set; }

        private int gameBoardLengthX;
        private int gameBoardLengthY;
        private Dictionary<int, TileIndex> TileValueIndex;


        /// <summary>
        /// Creates a new Game, if id is not provided, it will generate one.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="playersAndColors"></param>
        /// <param name="id"></param>
        public Game(GameSettings settings, Player[] players, Guid id)
        {
            this.Id = id;
            int seed = string.IsNullOrWhiteSpace(settings.seed) ? -1 : settings.seed.GetHashCode();
            this.CreateBoard(settings.BoardWidth, settings.BoardHeight, seed);
            this.gameBoardLengthX = GameBoard.GetLength(0);
            this.gameBoardLengthY = GameBoard.GetLength(1);
            this.CardDrawPile = settings.Cards;
            this.CardDrawPile.Shuffle();
            this.CardDiscardPile = new Stack<int>(CardDrawPile.Count + 1);
            foreach (Player player in players)
            {
                PlayerGameState state = new PlayerGameState(player.Name, settings.PlayerPiecesAmount, (PlayerColor)player.PlayerColor, CreateHand(settings.PlayerCardHoldAmount));
                this.Players[player.PlayerColor] = state;
            }
        }

        /// <summary>
        /// If the player has no pieces on hand, use <see cref="PlacePieceFromBoard(string, int, int, int, int)"/>
        /// <para>Places the specified players piece at GameBoard[<paramref name="boardX"/>, <paramref name="boardY"/>]</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="boardX"></param>
        /// <param name="boardY"></param>
        /// <returns>A bool indicating if it was successful</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <remarks>Throws an exception if the given indexes are outside the board</remarks>
        public bool PlacePieceIndex(PlayerGameState player, int boardX, int boardY)
        {
            if (boardX < 0 || this.gameBoardLengthX <= boardX)
                throw new IndexOutOfRangeException("X Coord is outside the board");

            if (boardY < 0 || this.gameBoardLengthY <= boardY)
                throw new IndexOutOfRangeException("Y Coord is outside the board");

            if (player != null)
            {
                this.GameBoard[boardX, boardY].PlayerPresence[player.Player]++;
                player.PlayerPieces--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// If the player has more pieces on hand use <see cref="PlacePiece(string, int, int)"/>
        /// <para> Takes the piece from GameBoard[<paramref name="boardTakeX"/>, <paramref name="boardTakeY"/>] and places it in board[<paramref name="boardPlaceX"/>, <paramref name="boardPlaceY"/>]</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="boardPlaceX"></param>
        /// <param name="boardPlaceY"></param>
        /// <param name="boardTakeX"></param>
        /// <param name="boardTakeY"></param>
        /// <returns>A bool indicating if it was successful</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <remarks>Throws an exception if the given indexes are outside the board</remarks>
        public bool PlacePieceFromBoardIndex(PlayerGameState player, int boardPlaceX, int boardPlaceY, int boardTakeX, int boardTakeY)
        {
            if (boardTakeX < 0 || this.gameBoardLengthX <= boardTakeX)
                throw new IndexOutOfRangeException("X Coord is outside the board");

            if (boardTakeY < 0 || this.gameBoardLengthY <= boardTakeY)
                throw new IndexOutOfRangeException("Y Coord is outside the board");

            if (player != null && this.GameBoard[boardTakeX, boardTakeY].PlayerPresence.ContainsKey(player.Player))
            {
                this.GameBoard[boardTakeX, boardTakeY].PlayerPresence[player.Player]--;
                player.PlayerPieces++;
                return PlacePieceIndex(player, boardPlaceX, boardPlaceY);
            }

            return false;
        }


        /// <summary>
        /// If the player has no pieces on hand use <see cref="PlacePieceFromBoardTileValue(string, int, int)"/>
        /// <para>Finds the field where <paramref name="tileValue"/> is and places the given <paramref name="player"/>'s piece there</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tileValue"></param>
        /// <returns>A bool indicating if it was successful</returns>
        public bool PlacePieceTileValue(PlayerGameState player, int tileValue)
        {
            if (TryFindTileValueIndex(tileValue, out TileIndex index))
                return PlacePieceIndex(player, index.x, index.y);
            return false;
        }

        /// <summary>
        /// If the player has pieces on hand, use <see cref="PlacePieceTileValue(string, int)"/>
        /// <para>Finds the field where <paramref name="tileValueTake"/> is and removes the given <paramref name="player"/>'s piece</para>
        /// <para>Then it finds the field where <paramref name="tileValuePlace"/> is and places the given <paramref name="player"/>'s piece</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tileValuePlace"></param>
        /// <param name="tileValueTake"></param>
        /// <returns>A bool indicating if it was successful</returns>
        public bool PlacePieceFromBoardTileValue(PlayerGameState player, int tileValuePlace, int tileValueTake)
        {
            //int takeI = -1, takeJ = -1;
            if (TryFindTileValueIndex(tileValueTake, out TileIndex index1))
            {
                if (TryFindTileValueIndex(tileValuePlace, out TileIndex index2))
                {
                    return PlacePieceFromBoardIndex(player, index2.x, index2.y, index1.x, index1.y);
                }

            }
            return false;
        }

        public bool HasDrawAmount(int amount)
        {
            return CardDrawPile.Count >= amount;
        }

        public event EventHandler<DrawPileRefillEventArgs> CardDrawPileRefilled;

        protected virtual void OnCardDrawPileRefilled(DrawPileRefillEventArgs e)
        {
            EventHandler<DrawPileRefillEventArgs> handler = CardDrawPileRefilled;
            handler?.Invoke(this, e);
        }

        public void FillDrawPile()
        {
            Stack<int> discardCopy = this.CardDiscardPile;
            foreach (int card in CardDrawPile)
            {
                discardCopy.Push(card);
            }
            this.CardDiscardPile.Clear();
            discardCopy.Shuffle();
            this.CardDrawPile = discardCopy;
            OnCardDrawPileRefilled(new DrawPileRefillEventArgs() { DrawPile = discardCopy });
        }

        /// <summary>
        /// Uses the given Cards to place a player piece. Placement is the sum of all the cards used. If there are no more pieces on hand it will take a piece from the tile with <paramref name="takeFromTileValue"/>'s value.
        /// Takes the given cards and removes them from the player hand and puts them in the <see cref="CardDiscardPile"/> <strong>stack</strong>, it then refills from <see cref="CardDrawPile"/> <strong>stack</strong>. If there is not enough cards to refill the hand, the <em>draw pile</em> will be refilled from the <em>discard pile</em> and shuffled. This fires the <see cref="CardDrawPileRefilled"/> <strong>event</strong>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="cards"></param>
        /// <param name="takeFromTileValue"></param>
        /// <returns></returns>
        public bool PlayerUseCards(string player, List<int> cards, int takeFromTileValue = -1)
        {
            bool success = false;
            PlayerGameState state = Players.FirstOrDefault(q => q.Player == player);
            if (state != null && state.Cards.All(p => cards.Contains(p))) //Check to make sure the player is actually holding the cards we are told they're holding
            {
                // First we place the players piece
                int cardSum = cards.Sum();
                if (state.PlayerPieces > 0)
                {
                    success = PlacePieceTileValue(state, cardSum);
                }
                else
                {
                    if (takeFromTileValue > 0)
                        success = PlacePieceFromBoardTileValue(state, cardSum, takeFromTileValue);
                }
                if (success) // we only continue if we could place a piece
                {
                    // Second we discard the players cards
                    foreach (int card in cards)
                    {
                        this.CardDiscardPile.Push(card);
                    }
                    state.Cards.RemoveAll(p => cards.Contains(p));

                    //Third we check that we can draw more cards
                    if (!HasDrawAmount(cards.Count))
                    {
                        FillDrawPile();
                    }

                    // Fourth we draw the amount of cards that were used
                    for (int i = 0; i < cards.Count; i++)
                    {
                        state.Cards.Add(this.CardDrawPile.Pop());
                    }
                    state.Cards.Sort();
                }

            }
            return success;
        }

        public Tile[] GetPossibleCardPlays(string player)
        {
            // This is a horrible and inefficient method, do not use too often
            // TODONE: See if this can be improved | 27-04-2021 Unsure if improved, but maybe?
            ISet<TilePlay> possiblePlays = new HashSet<TilePlay>();
            PlayerGameState state = Players.FirstOrDefault(q => q.Player == player);
            if (state != null)
            {
                foreach (CardPlay card in state.GetAllCardPlays())
                {
                    if (TryFindTileValueIndex(card.TileToPlay, out TileIndex index))
                    {
                        possiblePlays.Add(new TilePlay(index.x, index.y, card.TileToPlay, card.CardsUsed));
                    }
                }
            }
            return possiblePlays.ToArray();
        }


        public bool TryFindTileValueIndex(int tileValue, out TileIndex index)
        {
            if (tileValue > 0 && tileValue <= gameBoardLengthX * gameBoardLengthY)
            {
                index = TileValueIndex[tileValue];
                return true;
            }
            index = TileIndex.Zero;
            return false;
        }


        // ---------------------------------------------- Private Methods ----------------------------------------------

        /// <summary>
        /// Draws the top cards from the drawpile and adds them to a list
        /// <para>Amount drawn are dependent on <paramref name="handSize"/></para>
        /// </summary>
        /// <param name="handSize"></param>
        /// <returns>
        /// The drawn cards from the drawpile
        /// </returns>
        private List<int> CreateHand(int handSize)
        {
            List<int> hand = new List<int>();
            for (int i = 0; i < handSize; i++)
            {
                hand.Add(this.CardDrawPile.Pop());
            }
            hand.Sort();
            return hand;

        }

        /// <summary>
        /// Creates a Board of <see cref="BoardTile"/>. If the seed is < 0 the board will have it's numbers randomly placed
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="seed"></param>
        private void CreateBoard(int width, int height, int seed = -1)
        {
            GameBoard = new BoardTile[width, height];
            TileValueIndex = new Dictionary<int, TileIndex>();
            List<int> tileValues = Enumerable.Range(1, width * height).ToList();
            if (seed > 0)
            {
                tileValues.Shuffle(seed);
            }
            else
            {
                tileValues.Shuffle();
            }
            Queue<int> removeValues = new Queue<int>(tileValues);
            for (int i = 0; i < gameBoardLengthX; i++)
                for (int j = 0; j < gameBoardLengthY; j++)
                {
                    int tile = removeValues.Dequeue();
                    GameBoard[i, j] = new BoardTile() { TileValue = tile };
                    TileValueIndex.Add(tile, new TileIndex() { x = i, y = j });
                }

        }
    }

    public class DrawPileRefillEventArgs : EventArgs
    {
        public Stack<int> DrawPile { get; set; }
    }

    public struct TileIndex
    {
        public int x, y;

        public static TileIndex Zero = new TileIndex() { x = 0, y = 0 };
    }
}