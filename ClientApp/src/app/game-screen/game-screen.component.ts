import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import Two from "two.js";
import { GameService } from "../game.service";

@Component({
   selector: "app-game-screen",
   templateUrl: "./game-screen.component.html",
   styleUrls: ["./game-screen.component.scss"],
})
export class GameScreenComponent implements OnInit {
   @ViewChild("gameCanvas", { static: false })
   private canvas: ElementRef<HTMLDivElement>;
   //public table: number[][];

   public screen: Two;

   constructor(private gameService: GameService) {
      //this.createBoard();
   }

   ngOnInit() {
      //console.log(this.table);
   }

   ngAfterViewInit(): void {
      console.log("Initializing Game Board");
      this.screen = new Two({
         width: this.canvas.nativeElement.clientWidth,
         height: this.canvas.nativeElement.clientHeight,
      }).appendTo(this.canvas.nativeElement);
      /*
      let rect = this.screen.makeRectangle(0, 0, 100, 100);
      rect.fill = "#ed3232";
      rect.noStroke();
      this.screen.update();
*/
      if (this.gameService.game) {
         const tileWidth = Math.min(this.screen.width, this.screen.height) / this.gameService.game.gameBoard.length;
         for (let i = 0; i < this.gameService.game.gameBoard.length; i++) {
            for (let j = 0; j < this.gameService.game.gameBoard[i].length; j++) {
               const num = this.gameService.game.gameBoard[i][j].tileValue;
               makeTile(this.screen, i * tileWidth, j * tileWidth, tileWidth, tileWidth, num.toString());
            }
         }
         const rect = new Two.Rectangle(0, 0, this.screen.width, this.screen.height);
         rect.noFill();
         rect.stroke = "#020024";
      }
      this.screen.update();
      console.log("Game Board Initialized");
   }
}

//TODO refactor, this makes a single tile
function makeTile(screen: Two, x: number, y: number, width: number, height: number, text: string | null) {
   const rect = screen.makeRectangle(x + width / 2, y + height / 2, width, height);
   rect.noFill();
   rect.stroke = "#020024";
   rect.linewidth = 5;

   //Anchors
   const anchorMiddle = new Two.Anchor(
      x + width / 2,
      y + height / 2,
      x + width / 2,
      y + height / 2,
      x + width / 2,
      y + height / 2,
      Two.Commands.move
   );
   const anchorTopLeft = new Two.Anchor(x, y, x, y, x, y, Two.Commands.move);
   const anchorBottomLeft = new Two.Anchor(x, y + height, x, y + height, x, y + height, Two.Commands.move);
   const anchorTopRight = new Two.Anchor(x + width, y, x + width, y, x + width, y, Two.Commands.move);
   const anchorBottomRight = new Two.Anchor(
      x + width,
      y + height,
      x + width,
      y + height,
      x + width,
      y + height,
      Two.Commands.line
   );

   //Triangles

   const red = [anchorTopLeft.clone(), anchorMiddle.clone(), anchorBottomLeft.clone()];
   const black = [anchorTopLeft.clone(), anchorTopRight.clone(), anchorMiddle.clone()];
   const grey = [anchorMiddle.clone(), anchorTopRight.clone(), anchorBottomRight.clone()];
   const white = [anchorBottomLeft.clone(), anchorMiddle.clone(), anchorBottomRight.clone()];

   const redPath = screen.makePath(red);
   redPath.fill = "#db0033";
   redPath.noStroke();

   const blackPath = screen.makePath(black);
   blackPath.fill = "#302f40";
   blackPath.noStroke();

   const greyPath = screen.makePath(grey);
   greyPath.fill = "#adadad";
   greyPath.noStroke();

   const whitePath = screen.makePath(white);
   whitePath.fill = "#fffafa";
   whitePath.noStroke();

   if (text) {
      const props: any = {
         stroke: "black",
         fill: "white",
      };
      const screenText = new Two.Text(text, width * 0.3 + x, height * 0.3 + y + width * 0.1, props);
      screenText.size = width * 0.5;
      screen.add(screenText);
   }
}
