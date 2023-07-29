import { Component, Input, OnInit } from '@angular/core';
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
  @Input() articles?: Articles; // Adăugăm Input decorator pentru a primi articolele din componenta părinte

  url?: string;
  isFavouritesPage?: boolean = false;
  isNotFavouritesPage?: boolean = true;
  articlesToShow?: Articles = [];
  itemsPerPage: number = 6;
  currentPage: number = 1;
  totalPages: number = 0;

  constructor(
    private readonly articlesService: ArticlesService,
    private readonly router: Router,
    private readonly httpService: HttpService
  ) {}

  ngOnInit() {
    if (this.articles) {
      // Dacă avem deja articole primite ca Input, setăm lista de articole în serviciul ArticlesService
      this.articlesService.setArticles(this.articles);
    } else {
      // Dacă nu avem articole primite ca Input, facem o cerere HTTP pentru a obține articolele
      this.httpService.getArticles().subscribe((data) => {
        this.articles = data;
        this.articlesService.setArticles(data);
      });
    }

    this.articlesService.searchArticles(''); // Afisăm inițial toate articolele
    this.articlesService.getFilteredArticles().subscribe((filteredArticles) => {
      this.totalPages = Math.ceil((filteredArticles?.length || 0) / this.itemsPerPage);
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
    this.articles = articlesReceived;
    this.articlesService.setArticles(this.articles);
  }

  private paginateArticles() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.articlesService.getFilteredArticles().subscribe((filteredArticles) => {
      this.articlesToShow = filteredArticles?.slice(startIndex, endIndex);
    });
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

  searchArticles(searchTerm: string) {
    this.articlesService.searchArticles(searchTerm);
  }
}
