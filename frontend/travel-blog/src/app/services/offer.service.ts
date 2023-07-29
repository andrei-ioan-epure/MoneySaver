import { Injectable } from '@angular/core';
import { Article } from '../blog/model/article';


@Injectable({
  providedIn: 'root',
})
export class OfferService {
  private data?: Article;

  setParameter(value: any) {
    this.data = value;
  }

  getParameter(): any {
    return this.data;
  }

  constructor() {}
}
