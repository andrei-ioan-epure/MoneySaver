import { Component } from '@angular/core';
import { AbstractControl, Form, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';

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

export class SignUpComponent {
  formGroup: FormGroup = new FormGroup({});
  username: FormControl = new FormControl('',[Validators.required]);
  email: FormControl = new FormControl('',[Validators.required,Validators.email]);
  fullName : FormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  password : FormControl = new FormControl('', [Validators.required, checkLength]);
  constructor(){}

  ngOnInit(){
    this.formGroup = new FormGroup({
      username:this.username,
      email: this.email,
      fullName: this.fullName,
      password: this.password,
    });
  }

  submitForm(){
    if (this.formGroup.valid){
      console.log(this.formGroup.value);
    }
  }
}
