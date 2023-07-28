import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { Subscription } from 'rxjs';
import { AbstractControl, Form, FormControl, FormGroup, ValidationErrors, Validators} from '@angular/forms';

@Component({
  selector: 'app-edit-offer',
  templateUrl: './edit-offer.component.html',
  styleUrls: ['./edit-offer.component.scss']
})
export class EditOfferComponent {
  id!: number;

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
    private activatedRoute: ActivatedRoute,
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
       // Redirecționare către pagina de home
      setTimeout(() => {
        this.router.navigate(['blog']);
      }, 100);
    } else {
      console.log('Invalid');
      // this.router.navigate(['blog']);
    }
  }

  cancelEdit(){
    const url = this.activatedRoute.snapshot.url;
    this.id = Number(url[url.length - 2].path);
    this.router.navigate(['blog/article-list/full-offer/'+this.id])
  }
}
