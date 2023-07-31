import { Component, OnInit } from '@angular/core';
import { ArticlesService } from 'src/app/services/articles.service';
import { Observable, Subject, Subscriber } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent{
  ImagePath: string;
  public isLoggedIn: Observable<boolean>;
  public active: string="";

  constructor(
    private readonly authService: AuthService,
    private articleService: ArticlesService,
    private readonly router: Router,
  ) {
    this.ImagePath = '/assets/images/Logo.png';
    this.isLoggedIn = this.authService.isLoggedIn();

    this.router.events.subscribe(events => {
      if (events instanceof NavigationEnd) {
        this.active = this.router.url;
      }
    }); 
  }

  logOut(): void {
    this.authService.logOut();
  }

  performSearch(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value;
    this.articleService.setSearchTerm(searchTerm);
  }

  
}
