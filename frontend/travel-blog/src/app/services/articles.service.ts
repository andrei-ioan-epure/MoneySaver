import { Injectable } from '@angular/core';
import { Articles } from '../blog/model/article';


@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  articles: Articles = [
    {
      title: 'Romania',
      content: 'travel to Buharest',
      publishDate: new Date()
    },
    {
      title: 'Germany',
      content: 'travel to Berlin',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    }
    ,
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    }
    ,
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    },
    {
      title: 'France',
      content: 'travel to Paris',
      publishDate: new Date()
    }
  ];

  getArticles(): Articles {
    return this.articles;
  }
}
