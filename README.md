# Connect4

### Current stack:

-   [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0)
-   [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr)
-   [Angular 8](https://angular.io/)
-   [two.js](https://two.js.org/)

### Undecided tech:

-   Database, currently Entity Framework
-   User system, currently Identity Framework

# The purpose of this project

I am trying to recreate [a board game](https://www.spillehulen.dk/da/product/4) no longer in production created by Danspil

But mostly the purpose is to gain a deeper knowledge of the chosen frameworks. Some I have used before, some are new. I just want to improve myself.

---

**Rules:**

In the game, 2 to 4 players are playing Connect 4 but on a flat gameboard. You have your own pieces and are trying to get 4 tiles in a row, but multiple people can have their piece on the same tile, so you need to have the majority of pieces on one tile for it to be considered valid.

The way you place your piece is by using the four cards on your hand. Every tile on the board has a value. The standard board is 6 x 6 so the values go from 1 - 36. You can use your held cards to match the value on the tile and then place your piece. You can use one card, all four or something in between. You can add your cards together but not anything else. Once finished you discard the cards you used and draw new cards till you have four cards again.

---

## General design

The game will be served as a webapp. Same idea as [Secret Hitler](https://secrethitler.io/).

Every play by the user will be sent to the server to be calculated and confirmed, both to put the load on the server and to prevent cheating.
For this to be possible the server and client needs two way communication which goes against traditional RESTful design. To achieve this websockets are used, in this case, SignalR.

For the webapp, angular is used as a base and two.js is used for drawing the actual gameboard. The SignalR connections are kept in angular services and so is the state of the game. Two.js then draws the game from the game state
