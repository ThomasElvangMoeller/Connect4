import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";

@Component({
   selector: "app-main-menu",
   templateUrl: "./main-menu.component.html",
   styleUrls: ["./main-menu.component.scss"],
})
export class MainMenuComponent implements OnInit {
   public validating: boolean = false;
   public nameForm: FormControl = new FormControl("");
   constructor() {}

   ngOnInit() {}

   public hasUser() {}

   public async submit() {
      this.validating = true;
      //const response = await this.service.enterName();
      //if(response.success){
      //set user
      //} else {
      //this.errorMessage = response.error.message;
      //}
   }
}
