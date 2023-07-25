import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { HttpClient } from '@angular/common/http';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {
  hide = true;
  formGroup: FormGroup = new FormGroup({});
  email: FormControl = new FormControl('', [Validators.required, Validators.email]);
  password: FormControl = new FormControl('', [Validators.required, Validators.minLength(8)]);
  loginSuccessMessage = '';
  loginErrorMessage = '';

  constructor(private router: Router, private http: HttpClient,private authService: AuthenticationService) {}

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: this.email,
      password: this.password
    });
  }

  submitForm() {
    if (this.formGroup.valid) {
      const userLoginDto = {
        email: this.email.value,
        password: this.password.value
      };

      this.http.post<any>(`https://localhost:7207/api/User/login`, userLoginDto).subscribe(
        response => {
          // Dacă login-ul este cu succes, vei primi un mesaj în răspuns.
          console.log('Login success');
          this.authService.setIsLogged(true); // Setează starea de autentificare în serviciul de autentificare
          this.loginSuccessMessage = 'Logging is successful. You will be redirected to the main page in a few seconds.';
          this.loginErrorMessage = '';
          setTimeout(() => {
            this.router.navigate(['home']); // Redirecționare către pagina de home 
          }, 5000);
        },
        error => {
          
          console.error('Login failed:', error);
          this.authService.setIsLogged(false); // Setează starea de autentificare în serviciul de autentificare
          this.loginSuccessMessage = '';
          this.loginErrorMessage = 'Logging error. Please verify your authentication credentials.';
        }
      );
    }
  }
}