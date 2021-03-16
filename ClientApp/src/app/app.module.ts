import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { AppComponent } from "./app.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";
import { HomeComponent } from "./home/home.component";
//import { FetchDataComponent } from "./fetch-data/fetch-data.component";
//import { ApiAuthorizationModule } from "src/api-authorization/api-authorization.module";
//import { AuthorizeGuard } from "src/api-authorization/authorize.guard";
//import { AuthorizeInterceptor } from "src/api-authorization/authorize.interceptor";
import { GameChatComponent } from "./game-chat/game-chat.component";
import { MainMenuComponent } from "./main-menu/main-menu.component";
import { GameScreenComponent } from "./game-screen/game-screen.component";

@NgModule({
   declarations: [
      AppComponent,
      NavMenuComponent,
      HomeComponent,
      //FetchDataComponent,
      GameChatComponent,
      MainMenuComponent,
      GameScreenComponent,
   ],
   imports: [
      BrowserModule.withServerTransition({ appId: "adv_connect4" }),
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      //ApiAuthorizationModule,
      RouterModule.forRoot([
         { path: "", component: HomeComponent, pathMatch: "full" },
         { path: "gameplay", component: GameScreenComponent },
         // {
         //    path: "fetch-data",
         //    component: FetchDataComponent,
         //    canActivate: [AuthorizeGuard],
         // },
      ]),
   ],
   providers: [
      // {
      //    provide: HTTP_INTERCEPTORS,
      //    useClass: AuthorizeInterceptor,
      //    multi: true,
      // },
   ],
   bootstrap: [AppComponent],
})
export class AppModule {}
