import { Component } from '@angular/core';
import {  FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentification.service';


@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss']
})
export class SigninComponent {
  hide = true;
  formGroup: FormGroup = new FormGroup({});
  email: FormControl = new FormControl('',[Validators.required,Validators.email]);
  password : FormControl = new FormControl('', [Validators.required,Validators.minLength(8)]);
  constructor(private router: Router,private authService: AuthenticationService) {}

  ngOnInit(){
    this.formGroup = new FormGroup({
      email: this.email,
      password: this.password
    });
  }

  submitForm(){
    if (this.formGroup.valid) {
      this.authService.setIsLogged(true); 
      console.log(this.formGroup.value);
      setTimeout(() => {
        this.router.navigate(['home']); // Redirecționare către pagina de home 
      }, 3000); // Redirecționare către pagina de home
    }
    else
    {
      console.error('Login failed');
          this.authService.setIsLogged(false); 
    }

  }
}
