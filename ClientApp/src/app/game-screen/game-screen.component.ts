import {
   AfterViewInit,
   Component,
   ElementRef,
   OnInit,
   ViewChild,
} from "@angular/core";
import { WebGlService } from "../web-gl.service";

@Component({
   selector: "app-game-screen",
   templateUrl: "./game-screen.component.html",
   styleUrls: ["./game-screen.component.scss"],
})
export class GameScreenComponent implements OnInit {
   //@ViewChild("gameCanvas", { static: false })
   //private canvas: ElementRef<HTMLCanvasElement>;
   public table: number[][];

   constructor() {
      let linearValues: number[] = [];
      for (let i = 1; i < 37; ++i) linearValues[i] = i;
      linearValues = shuffle(linearValues);
      this.table = new Array<number[]>();
      this.table.length = 6;
      for (let i = 0; i < this.table.length; i++) {
         this.table[i] = [];
         this.table[i].length = 6;
         for (let j = 0; j < this.table[i].length; j++) {
            this.table[i][j] = linearValues.pop();
         }
      }
      console.log(linearValues);
      console.log(this.table);
   }

   ngOnInit() {
      //console.log(this.table);
   }
   /**
   ngAfterViewInit(): void {
      if (!this.canvas) {
         alert("Canvas not supplied! cannot bind WebGl to context!");
         return;
      }
      this.webglService.initializeWebGlContext(this.canvas.nativeElement);
   }
   */
}

function shuffle<T>(array: T[]): T[] {
   let tmp: T,
      current: number,
      top: number = array.length;
   if (top)
      while (--top) {
         current = Math.floor(Math.random() * (top + 1));
         tmp = array[current];
         array[current] = array[top];
         array[top] = tmp;
      }
   return array;
}
