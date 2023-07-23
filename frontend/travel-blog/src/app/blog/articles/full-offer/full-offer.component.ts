import { Component, Input, OnInit } from '@angular/core';
import { OfferService } from 'src/app/services/offer.service';
import { Offer } from './model/offer';
import { of } from 'rxjs';

@Component({
  selector: 'app-full-offer',
  templateUrl: './full-offer.component.html',
  styleUrls: ['./full-offer.component.scss']
})
export class FullOfferComponent implements OnInit{
  @Input() image?:string;
  @Input() title?: string;
  @Input() city?:string;
  @Input() expiration?:Date;
  @Input() code?:string;
  @Input() category?:string;
  @Input() author?:string;
  @Input() posted?:Date;
  @Input() content?: string;

  constructor(private offerService :OfferService){}

  ngOnInit(): void {
    const offer=this.offerService.getParameter() as Offer;
    this.title=offer.title;
    this.content=offer.content;
    this.city=offer.city;
    this.expiration=offer.expiration;
    this.code=offer.code;
    this.category=offer.category;
    this.author=offer.author;
    this.posted=offer.posted;
  }
}


