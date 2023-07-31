import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Article } from '../model/article';
import { HttpService } from '../../services/http.service';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';

function checkLength(c: AbstractControl): ValidationErrors | null {
  let stringControl: string = c.value;
  if (stringControl.length >= 8) {
    return null;
  }
  return { length: true };
}

@Component({
  selector: 'app-add-offer',
  templateUrl: './add-offer.component.html',
  styleUrls: ['./add-offer.component.scss'],
})
export class AddOfferComponent {
  imagePath:string;
  articlesCreateSubscription!: Subscription;
  hide = true;
  formGroup: FormGroup = new FormGroup({});
  offername: FormControl = new FormControl('', [Validators.required]);
  about: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(5),
  ]);
  category: FormControl = new FormControl('', [
    Validators.required,
  ]);
  store: FormControl = new FormControl('', [
    Validators.required,
  ]);
  city: FormControl = new FormControl('', [
    Validators.required,
  ]);
  expiredate: FormControl = new FormControl('', [Validators.required]);
  code: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(4),
  ]);
  constructor(
    private router: Router,
    private readonly httpService: HttpService,
    private readonly authService : AuthService
  ) {this.imagePath='/assets/images/addOffer.png'}

  ngOnInit() {
    this.formGroup = new FormGroup({
      offername: this.offername,
      about: this.about,
      category: this.category,
      city: this.city,
      expiredate: this.expiredate,
      code: this.code,
      store:this.store
    });
  }

  submitForm() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;
      const newArticle = {
        title: formData.offername,
        posted: new Date(),
        city: formData.city,
        expiration: formData.expiredate,
        category: formData.category,
        code: formData.code,
        store: formData.store, 
        author: this.authService.getFullName() as string,
        content: formData.about,
        creatorId: this.authService.getId() as number,
      };
      console.log(newArticle);
      this.articlesCreateSubscription = this.httpService
        .postArticle(newArticle)
        .subscribe((value) => {
          console.log(value);
        });
      console.log('New article added');
       // Redirecționare către pagina de home
      setTimeout(() => {
        this.router.navigate(['blog']);
      }, 250);
    } else {
      console.log('Invalid');
      // this.router.navigate(['blog']);
    }
  }
}
