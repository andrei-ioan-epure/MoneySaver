import { Component, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { Subscription } from 'rxjs';
import {
  AbstractControl,
  Form,
  FormControl,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { Article } from 'src/app/blog/model/article';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-edit-offer',
  templateUrl: './edit-offer.component.html',
  styleUrls: ['./edit-offer.component.scss'],
})
export class EditOfferComponent{
  imagePath:string;
  id!: number;
  article?: Article;
  userFullName: string | null=null;
  userId: number | null =null;

  
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
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private readonly httpService: HttpService,
    private readonly authService: AuthService
  ) {this.imagePath='/assets/images/editOffer.png'}

 

  ngOnInit() {
    this.formGroup = new FormGroup({
      offername: this.offername,
      about: this.about,
      category: this.category,
      city: this.city,
      expiredate: this.expiredate,
      code: this.code,
      store: this.store,
    });
    const url = this.activatedRoute.snapshot.url;
    this.id = Number(url.toString().split(',')[2]);

    this.httpService.getArticle(this.id).subscribe((article) => {
      this.article = {
        title: article.title,
        content: article.content,
        city: article.city,
        expiration: article.expiration,
        code: article.code,
        category: article.category,
        author: article.author,
        posted: article.posted,
        store: article.store,
        creatorId: article.creatorId,
      };

      this.offername.setValue(this.article.title);
      this.about.setValue(this.article.content);
      this.city.setValue(this.article.city);
      this.category.setValue(this.article.category);
      this.expiredate.setValue(this.article.expiration);
      this.code.setValue(this.article.code);
      this.store.setValue(this.article.store);

      this.userFullName = this.authService.getFullName();
      this.userId = this.authService.getId();
    });
  }

  editOffer() {
    if (this.formGroup.valid) {
      const formData = this.formGroup.value;
      const item: Article = {
        title: formData.offername,
        posted: new Date(),
        city: formData.city,
        expiration: formData.expiredate,
        category: formData.category,
        code: formData.code,
        store: formData.store,
        author: this.userFullName as string, //get from logged user
        content: formData.about,
        creatorId: this.userId as number, //get from logged user
      };
      console.log(item);
      this.httpService.putArticle(this.id, item);

      setTimeout(() => {
        this.router.navigate(['blog/article-list/full-offer/' + this.id]);
      }, 300);
    } else {
      console.log('Invalid');
      // this.router.navigate(['blog']);
    }
  }

  cancelEdit() {
    const url = this.activatedRoute.snapshot.url;
    this.id = Number(url[url.length - 2].path);
    this.router.navigate(['blog/article-list/full-offer/' + this.id]);
  }
}
