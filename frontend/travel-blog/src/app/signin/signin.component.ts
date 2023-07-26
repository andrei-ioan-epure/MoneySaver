import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(private router: Router, private authService: AuthenticationService) {}

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: this.email,
      password: this.password
    });
  }

  submitForm() {
    if (this.formGroup.valid) {
      this.authService.login(this.email.value, this.password.value).subscribe(
        success => {
          if (success) {
            console.log('Login success');
            this.loginSuccessMessage = 'Logging is successful. You will be redirected to the main page in a few seconds.';
            this.loginErrorMessage = '';
            setTimeout(() => {
              this.router.navigate(['home']);
            }, 5000);
          } else {
            console.error('Login failed');
            this.loginSuccessMessage = '';
            this.loginErrorMessage = 'Logging error. Please verify your authentication credentials.';
          }
        }
      );
    }
  }

  logout() {
    this.authService.logout();  }
}
