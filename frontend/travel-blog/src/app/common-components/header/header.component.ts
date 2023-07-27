import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/authentification.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  ImagePath: string;
  isLogoutMenuOpen = false; // Variabila pentru a urmări dacă meniul de logout este deschis sau nu

  constructor(private authService: AuthenticationService) {
    this.ImagePath = '/assets/images/Logo.png';
  }

  get isLogged(): boolean {
    return this.authService.getIsLogged();
  }

  toggleLogoutMenu() {
    this.isLogoutMenuOpen = !this.isLogoutMenuOpen; // Inversăm starea meniului de logout la fiecare clic pe butonul de log out
  }

  logout() {
    this.authService.setIsLogged(false); // Realizează deconectarea în serviciul de autentificare
    this.isLogoutMenuOpen = false; // Închidem meniul de logout după ce utilizatorul s-a deconectat
  }
}
