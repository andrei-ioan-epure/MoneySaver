import { Component, OnInit } from '@angular/core';

import { Articles } from '../model/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})

export class ArticleComponent implements OnInit {
  articles?: Articles;
  url?:string;
  isFavouritesPage?:boolean=false;

  constructor(private readonly articlesService: ArticlesService, private readonly router:Router) {}

  ngOnInit() {
      this.articles = this.articlesService.getArticles();
      this.url=this.router.url;
      if(this.url.includes('favourites')){
        this.isFavouritesPage=true;
      }

  }
  onArticleChange(value: string): void {
    console.log('clicked on', value);
  }
  
}