import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../blog/model/user';
import { SignInService } from '../services/sign-in.service';

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
    private readonly signInService: SignInService,
    private router: Router
  ) {}

  ngOnInit() {
    this.formGroup = new FormGroup({
      email: this.email,
      password: this.password,
    });
  }

  submitForm() {
    if (this.formGroup.valid) {
      //console.log(this.formGroup.value);
      const user: User = {
        userName: 'string',
        fullName: 'string',
        email: this.formGroup.controls['email'].value,
        password: this.formGroup.controls['password'].value,
        isCreator: false,
      };
      //console.log(userLogin);

      this.signInService.LogIn(user).subscribe(() => {
        console.log('User is logged in');
      });
      //this.router.navigate(['home']); // Redirecționare către pagina de home
    }
  }
}
