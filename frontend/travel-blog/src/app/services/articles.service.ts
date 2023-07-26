import { Injectable } from '@angular/core';
import { Articles } from '../blog/model/article';


@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
   articles: Articles = [];
 
  getArticles(): Articles {
    return this.articles;
  }
}
