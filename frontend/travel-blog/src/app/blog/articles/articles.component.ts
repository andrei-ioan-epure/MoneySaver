import { Component, OnInit } from '@angular/core';

import { Articles } from '../model/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss'],
})
export class ArticleComponent implements OnInit {
  articles?: Articles;
  url?: string;
  isFavouritesPage?: boolean = false;
  isNotFavouritesPage?: boolean = true;

  constructor(
    private readonly articlesService: ArticlesService,
    private readonly router: Router,
    private readonly httpService: HttpService
  ) {}

  ngOnInit() {
    //  this.articles = this.articlesService.getArticles();
    this.httpService.getArticles().subscribe((data) =>
      //console.log(data));
      {
        this.articles = data;
      }
    );
    this.url = this.router.url;
    if (this.url.includes('favourites')) {
      this.isFavouritesPage = true;
      this.isNotFavouritesPage = !this.isFavouritesPage;
    } else {
      this.httpService
        .getArticles()
        .subscribe((data) => (this.articles = data));
    }
  }
  onArticleChange(value: string): void {
    console.log('clicked on', value);
  }
}
