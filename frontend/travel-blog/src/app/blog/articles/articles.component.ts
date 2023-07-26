import { Component, Input, OnInit } from '@angular/core';

import { Articles } from '../model/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticleComponent implements OnInit {
  articles?: Articles;
  url?:string;
  isFavouritesPage?:boolean=false;
  isNotFavouritesPage?:boolean=true;
  articlesToShow?: Articles = [];
  itemsPerPage: number = 6;
  currentPage: number = 1;
  totalPages: number = 0; // Declare totalPages variable


  constructor(private readonly articlesService: ArticlesService, private readonly router:Router,private readonly httpService:HttpService) {}

  ngOnInit() {
    this.httpService.getArticles().subscribe((data) => {
      this.articles = data;
      this.totalPages = Math.ceil((this.articles?.length || 0) / this.itemsPerPage); // Actualizează numărul total de pagini
      this.paginateArticles();
    });
  
    this.url = this.router.url;
    if (this.url.includes('favourites')) {
      this.isFavouritesPage = true;
      this.isNotFavouritesPage = !this.isFavouritesPage;
    }
  }
  
  
  onArticleChange(value: string): void {
    console.log('clicked on', value);
  }
  onDataReceived(articlesReceived: Articles) {
    console.log('Articles received from Child:', articlesReceived);
    this.articles = articlesReceived; // Store the received articles in the parent component
    this.paginateArticles();
  }
  private paginateArticles() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.articlesToShow = this.articles?.slice(startIndex, endIndex);
  }

  previousPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.paginateArticles();
    }
  }

  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.paginateArticles();
    }
  }
  
}



