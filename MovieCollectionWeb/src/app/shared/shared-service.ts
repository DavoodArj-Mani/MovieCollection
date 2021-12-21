import { EventEmitter, Injectable, Output } from "@angular/core";
@Injectable({
    providedIn: 'root' // just before your class
})
export class SharedService {
   @Output() userName: string;
   @Output() isLogedIn: Boolean;

   constructor() {
   }
   setUserName(userName) {
    this.userName = userName;
   }
   getUserName() {
     return this.userName
   }

   setLoginFlag(flag)
   {
    this.isLogedIn = flag;
   }
   getLoginFlag()
   {
    return this.isLogedIn;
   }
} 