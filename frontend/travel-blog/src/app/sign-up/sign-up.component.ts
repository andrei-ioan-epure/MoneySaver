import { Component, OnDestroy } from '@angular/core';
import { AbstractControl, Form, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { SignUpService } from '../services/sign-up.service';
import { User } from '../blog/model/user';
import { Subscription } from 'rxjs';
import { Route, Router } from '@angular/router';
import { tick } from '@angular/core/testing';

function checkLength(c: AbstractControl): ValidationErrors|null{
  let stringControl:string = c.value;
  if(stringControl.length>=8){
    return null;
  }
  return {'length':true};
}

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})

export class SignUpComponent{
  hide = true;
  
  formGroup: FormGroup = new FormGroup({});
  username: FormControl = new FormControl('',[Validators.required]);
  email: FormControl = new FormControl('',[Validators.required,Validators.email]);
  fullName : FormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  password : FormControl = new FormControl('', [Validators.required, checkLength]);

  userCreateSubscription!:Subscription

  constructor(
    private readonly signUpService: SignUpService,
    private router: Router
    ){}

  ngOnInit(){
    this.formGroup = new FormGroup({
      username:this.username,
      email: this.email,
      fullName: this.fullName,
      password: this.password,
    });
  }

  submitForm(): void{
    if (this.formGroup.valid){
      const user: User = {
        userName  : this.formGroup.controls['username'].value,
        fullName  : this.formGroup.controls['fullName'].value,
        email     : this.formGroup.controls['email'].value,
        password  : this.formGroup.controls['password'].value,
        isCreator : false
      };
      this.userCreateSubscription = this.signUpService.postUser(user).subscribe();

      this.router.navigate(['/sign-up-response']);
    }
  }
}
