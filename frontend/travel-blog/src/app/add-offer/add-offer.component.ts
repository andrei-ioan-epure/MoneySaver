import { Component } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { Article } from '../blog/model/article';
import { HttpService } from '../services/http.service';
import { Subscription } from 'rxjs';

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
    Validators.minLength(4),
  ]);
  store: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(5),
  ]);
  city: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(1),
  ]);
  expiredate: FormControl = new FormControl('', [Validators.required]);
  code: FormControl = new FormControl('', [
    Validators.required,
    Validators.minLength(4),
  ]);
  constructor(
    private router: Router,
    private readonly httpService: HttpService
  ) {}

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
        store: 'Glovo', //Add store field in form
        author: 'Epure Andrei-Ioan',
        content: formData.about,
        creatorId: 5,
      };
      console.log(newArticle);
      this.articlesCreateSubscription = this.httpService
        .postArticle(newArticle)
        .subscribe((value) => {
          console.log(value);
        });
      console.log('New article added');
      this.router.navigate(['blog']); // Redirecționare către pagina de home
    } else {
      console.log('Invalid');
      // this.router.navigate(['blog']);
    }
  }
}
