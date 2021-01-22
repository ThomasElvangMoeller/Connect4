import { Injectable } from "@angular/core";

@Injectable({
   providedIn: "root",
})
export class WebGlService {
   private _renderingContext: RenderingContext;
   private get gl(): WebGLRenderingContext {
      return this._renderingContext as WebGLRenderingContext;
   }
   constructor() {}

   initializeWebGlContext(canvas: HTMLCanvasElement) {
      this._renderingContext =
         canvas.getContext("webgl") || canvas.getContext("experimental-webgl");
      if (!this.gl) {
         alert("unable to initialize WebGl. Your browser may not support it");
         return;
      }
      this.setWebGlCanvasDimensions(canvas);
      this.initializeWebGlCanvas();
   }

   setWebGlCanvasDimensions(canvas: HTMLCanvasElement): void {
      this.gl.canvas.width = canvas.clientWidth;
      this.gl.canvas.height = canvas.clientHeight;
   }

   initializeWebGlCanvas(): void {
      //set clear color to black, fully opaque
      this.gl.clearColor(0, 0, 0, 1);
      // enable depth testing
      this.gl.enable(this.gl.DEPTH_TEST);
      // Near things obscure far things
      this.gl.depthFunc(this.gl.LEQUAL);
      // Clear the colour as well as the depth buffer.
      this.gl.clear(this.gl.COLOR_BUFFER_BIT | this.gl.DEPTH_BUFFER_BIT);
   }
}
