import { Component, OnInit } from '@angular/core';

import { Articles } from '../model/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})

export class ArticleComponent implements OnInit {
  articles?: Articles;

  constructor(private readonly articlesService: ArticlesService) {}

  ngOnInit() {
      this.articles = this.articlesService.getArticles();
  }
  onArticleChange(value: string): void {
    console.log('clicked on', value);
  }
}