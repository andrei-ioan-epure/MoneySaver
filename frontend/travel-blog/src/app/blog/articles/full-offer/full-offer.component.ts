import { Component, Input, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { Article } from '../../model/article';

@Component({
  selector: 'app-full-offer',
  templateUrl: './full-offer.component.html',
  styleUrls: ['./full-offer.component.scss'],
})
export class FullOfferComponent implements OnInit {
  id!: number;
  image?: string;
  title?: string;
  city?: string;
  expiration?: Date;
  code?: string;
  category?: string;
  author?: string;
  posted?: Date;
  content?: string;
  store?: string;
  creatorId?: number;

  constructor(
    private offerService: OfferService,
    private activatedRoute: ActivatedRoute,
    private httpService: HttpService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const offer = this.offerService.getParameter() as Article;
    if (offer) {
      this.setArticle(offer);
      const url = this.activatedRoute.snapshot.url;
      this.id = Number(url[url.length - 1].path);
    } else {
      const url = this.activatedRoute.snapshot.url;
      this.id = Number(url[url.length - 1].path);
      this.httpService.getArticle(this.id).subscribe((article) => {
        this.setArticle(article);
      });
    }

    console.log(this.id);
  }

  private setArticle(offer: Article) {
    this.title = offer.title;
    this.content = offer.content;
    this.city = offer.city;
    this.expiration = offer.expiration;
    this.code = offer.code;
    this.category = offer.category;
    this.author = offer.author;
    this.posted = offer.posted;
    this.store = offer.store;
    this.creatorId = offer.creatorId;
  }

  editOffer() {
    const item: Article = {
      title: 'test212',
      posted: this.posted!,
      city: this.city!,
      expiration: this.expiration!,
      category: this.category!,
      code: this.code!,
      store: this.store!,
      author: 'test',
      content: this.content!,
      creatorId: this.creatorId!,
    };
    console.log(item);
    this.httpService.putArticle(this.id, item);
    
    setTimeout(() => {
      window.location.reload();
    }, 100);
  }
  deleteOffer() {
    console.log('Delete article');
    console.log(this.id);
    this.httpService.deleteArticle(this.id);
    setTimeout(() => {
      this.router.navigate(['blog']);
    }, 100);
  }
}
