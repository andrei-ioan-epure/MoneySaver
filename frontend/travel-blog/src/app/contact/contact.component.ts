import { Component } from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AbstractControl, Form, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';
import { Router } from '@angular/router';

function checkLength(c: AbstractControl): ValidationErrors|null{
  let stringControl:string = c.value;
  if(stringControl.length>=8){
    return null;
  }
  return {'length':true};
}

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss'],
  })

export class ContactComponent {
  formGroup:FormGroup=new FormGroup({});
  name: FormControl=new FormControl('',[Validators.required, Validators.minLength(5)]);
  email:FormControl=new FormControl('',[Validators.required,Validators.email]);
  msgsj:FormControl=new FormControl('',[Validators.required]);
  message:FormControl=new FormControl('',[Validators.required]);
  imagePath:string;
  constructor(private router:Router){this.imagePath='/assets/images/contact.png'}

  ngOnInit(){
    this.formGroup=new FormGroup({
      name:this.name,
      email:this.email,
      msgsj:this.msgsj,
      message:this.message
    });
  }


  submitForm(){
    //this.formGroup.markAllAsTouched;
    if(this.formGroup.valid){
      this.router.navigateByUrl('/contact-response');
      console.log(this.formGroup.value);
    }
    
  }
}
