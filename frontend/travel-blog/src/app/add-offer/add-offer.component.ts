import { Component } from '@angular/core';
import {  AbstractControl, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';


function checkLength(c: AbstractControl): ValidationErrors|null{
  let stringControl:string = c.value;
  if(stringControl.length>=8){
    return null;
  }
  return {'length':true};
}


@Component({
  selector: 'app-add-offer',
  templateUrl: './add-offer.component.html',
  styleUrls: ['./add-offer.component.scss']
})


export class AddOfferComponent {
  hide = true;
  formGroup: FormGroup = new FormGroup({});
  offername: FormControl = new FormControl('',[Validators.required]);
  about: FormControl = new FormControl('', [Validators.required, Validators.minLength(5)]);
  category : FormControl = new FormControl('', [Validators.required, Validators.minLength(4)]);
  city : FormControl = new FormControl('', [Validators.required, Validators.minLength(1)]);
  expiredate : FormControl = new FormControl('', [Validators.required]);
  code : FormControl = new FormControl('', [Validators.required, Validators.minLength(4)]);
 // image: FormControl=new FormControl('',[Validators.required]);
  constructor(private router: Router) {}

  ngOnInit(){
    this.formGroup = new FormGroup({
      offername:this.offername,
      about:this.about,
      category:this.category,
      city:this.city,
      expiredate:this.expiredate,
      code:this.code,
     // image:this.image,
    });
  }

  submitForm(){
    if (this.formGroup.valid) {
      console.log(this.formGroup.value);
      this.router.navigate(['blog']); // Redirecționare către pagina de home
    }
    else{
      this.router.navigate(['blog']);
    }

  }
}
