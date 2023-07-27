import { Injectable } from '@angular/core';
import { Offer } from '../blog/articles/full-offer/model/offer';

@Injectable({
  providedIn: 'root',
})
export class OfferService {
  // offer: Offer = {
  //   image: '/assets/images/Andrei.jpg',
  //   title: 'SUMMER SALE',
  //   city: 'Iași',
  //   expiration: new Date('07.09.2023'),
  //   code: 'EXTRA20',
  //   category: 'Fashion',
  //   author: 'Diaconu Axana-Marinela',
  //   posted: new Date('08.07.2023'),
  // };

  private data?: Offer;

  setParameter(value: any) {
    this.data = value;
  }

  getParameter(): any {
    return this.data;
  }

  constructor() {}
}
