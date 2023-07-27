import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentification.service';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  ImagePath: string;
  isLogoutMenuOpen = false;

  constructor(
    private authService: AuthenticationService,
    private articleService: ArticlesService
  ) {
    this.ImagePath = '/assets/images/Logo.png';
  }

  get isLogged(): boolean {
    return this.authService.getIsLogged();
  }

  toggleLogoutMenu() {
    this.isLogoutMenuOpen = !this.isLogoutMenuOpen;
  }

  logout() {
    this.authService.setIsLogged(false);
    this.isLogoutMenuOpen = false;
  }

  performSearch(event: Event) {
    const searchTerm = (event.target as HTMLInputElement).value;
    this.articleService.setSearchTerm(searchTerm);
  }
}
