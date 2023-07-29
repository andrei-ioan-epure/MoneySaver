import { Component } from '@angular/core';
import { ArticlesService } from 'src/app/services/articles.service';
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

  constructor(private readonly authService:AuthService,private articleService: ArticlesService){
    this.ImagePath='/assets/images/Logo.png';
    this.isLoggedIn=this.authService.isLoggedIn();
  }

  

  

  

  logOut():void {
    this.authService.logOut();
  }
  performSearch(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value;
    this.articleService.setSearchTerm(searchTerm);
  }
}
