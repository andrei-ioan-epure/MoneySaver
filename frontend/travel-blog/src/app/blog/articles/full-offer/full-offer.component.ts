import { Component, Input, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { Offer } from './model/offer';
import { of } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';

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
    const offer = this.offerService.getParameter() as Offer;
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

  private setArticle(offer: Offer) {
    this.title = offer.title;
    this.content = offer.content;
    this.city = offer.city;
    this.expiration = offer.expiration;
    this.code = offer.code;
    this.category = offer.category;
    this.author = offer.author;
    this.posted = offer.posted;
  }
  // item = {
  //   title: this.title,
  //   posted: this.posted,
  //   city: this.city,
  //   expiration: this.expiration,
  //   category: this.category,
  //   code: this.code,
  //   store: thiss.store,
  //   author: this.author,
  //   content: this.content,
  //   creatorId: this.creatorId,
  // };

  editOffer() {}
  deleteOffer() {
    console.log('Delete article');
    console.log(this.id);
    this.httpService.deleteArticle(this.id);
    setTimeout(() => {
      this.router.navigate(['blog']);
    }, 10);
  }
}
