import { Component } from '@angular/core';
import { ArticlesService } from 'src/app/services/articles.service';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  ImagePath: string;
  public isLoggedIn: Observable<boolean>;
  public active: string = '';
  public user: string = ''; // Variabila pentru a stoca initiala utilizatorului logat
  public showLogout: boolean = false;

  constructor(
    private readonly authService: AuthService,
    private articleService: ArticlesService,
    private readonly router: Router
  ) {
    this.ImagePath = '/assets/images/Logo.png';
    this.isLoggedIn = this.authService.isLoggedIn();

    this.router.events.subscribe((events) => {
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

  // Metoda pentru a obține inițiala din username ul utilizatorului
  getInitials(): string {
    const userEmail = this.authService.getUserInitials();
    if (!userEmail) return '';

    const username = userEmail.trim().split('@')[0];
    const nameInitials = username
      .split('.')
      .map((namePart) => namePart.charAt(0).toUpperCase())
      .join('');

    return nameInitials;
  }
  onClickIcon(): void {
    this.showLogout = !this.showLogout;
  }
}
