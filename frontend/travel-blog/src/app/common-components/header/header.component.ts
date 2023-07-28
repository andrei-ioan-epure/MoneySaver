import { Component } from '@angular/core';
import { Observable, Subject, Subscriber } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {

  ImagePath:string;
  public isLoggedIn : Observable<boolean>;

  constructor(private readonly authService:AuthService){
    this.ImagePath='/assets/images/Logo.png';
    this.isLoggedIn=this.authService.isLoggedIn();
  }

  logOut():void {
    this.authService.logOut();
  }
}
