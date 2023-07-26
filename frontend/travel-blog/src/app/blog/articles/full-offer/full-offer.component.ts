import { Component, Input, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { Offer } from './model/offer';
import { of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
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

  constructor(
    private offerService: OfferService,
    private activatedRoute: ActivatedRoute,
    private httpService: HttpService
  ) {}

  ngOnInit(): void {
    const offer = this.offerService.getParameter() as Offer;
    if (offer) {
      this.setArticle(offer);
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
}
