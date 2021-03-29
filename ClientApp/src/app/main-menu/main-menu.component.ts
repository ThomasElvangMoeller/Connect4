import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormControl, FormGroup } from "@angular/forms";
import { UserService } from "../user.service";

@Component({
   selector: "app-main-menu",
   templateUrl: "./main-menu.component.html",
   styleUrls: ["./main-menu.component.scss"],
})
export class MainMenuComponent implements OnInit {
   public validating: boolean = false;
   public errorMessage: string | null;
   public nameForm: FormGroup = new FormGroup({ name: new FormControl("") });
   constructor(private userService: UserService) {}

   ngOnInit() {}

   public hasUser() {}

   public async submit() {
      this.validating = true;
      const name = this.nameForm.controls["name"].value;
      const response = await this.userService.validate(name);
      if (response.valid) {
         this.userService.setUser({ name, connectionId: null, applicationUserGuid: null });
         //set user
      } else {
         this.errorMessage = response.message;
      }
   }
}
