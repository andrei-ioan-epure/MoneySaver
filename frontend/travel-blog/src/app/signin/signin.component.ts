import { Component } from '@angular/core';
import {  FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

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
  constructor(private router: Router) {}

  ngOnInit(){
    this.formGroup = new FormGroup({
      email: this.email,
      password: this.password
    });
  }

  submitForm(){
    if (this.formGroup.valid) {
      console.log(this.formGroup.value);
      this.router.navigate(['home']); // Redirecționare către pagina de home
    }

  }
}
