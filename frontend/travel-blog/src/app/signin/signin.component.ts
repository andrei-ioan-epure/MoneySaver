import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User, UserLogin } from '../blog/model/user';
import { AuthService } from '../services/auth.service';
import { EMPTY, catchError, of } from 'rxjs';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent {
  hide = true;
  formGroup: FormGroup = new FormGroup({});
  email: FormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  password: FormControl = new FormControl('', [Validators.required]);
  constructor(
    private readonly authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authService.logOut();
    this.formGroup = new FormGroup({
      email: this.email,
      password: this.password,
    });
  }

  submitForm() {
    if (this.formGroup.valid) {
      const user: UserLogin = {
        email: this.formGroup.controls['email'].value,
        password: this.formGroup.controls['password'].value,
      };

      this.authService
        .LogIn(user)
        .pipe(
          catchError((err) => {
            console.log('Auth failed', err);
            return EMPTY;
          })
        )
        .subscribe(() => {
          console.log('User logged in');
          this.router.navigate(['home']);
        });
    }
  }
}
