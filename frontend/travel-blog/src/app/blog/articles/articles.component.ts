import { Component, Input, OnInit } from '@angular/core';
import { Articles } from '../model/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { AuthService } from 'src/app/services/auth.service';
import { Observable } from 'rxjs';

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
  isFavorite: Map<number, boolean> = new Map<number, boolean>();
  favoriteArticles?: Articles = [];
  articlesToShow?: Articles = [];
  itemsPerPage: number = 6;
  currentPage: number = 1;
  totalPages: number = 1;
  public isLoggedIn: Observable<boolean>;
  constructor(
    private readonly articlesService: ArticlesService,
    private readonly router: Router,
    private readonly httpService: HttpService,
    private readonly authService: AuthService
  ) {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  ngOnInit() {
    this.httpService
      .getFavoriteArticles(this.authService.getId() as number)
      .subscribe((data) => {
        this.favoriteArticles = data;

        this.url = this.router.url;
        if (this.url.includes('favourites')) {
          this.articlesToShow = this.favoriteArticles;
          this.articlesService.setArticles(this.favoriteArticles);

          this.isFavouritesPage = true;
          this.isNotFavouritesPage = !this.isFavouritesPage;
        } else {
          this.httpService.getArticles().subscribe((data) => {
            this.articlesToShow = data;
            this.articlesService.setArticles(data);

            for (var item of this.articlesToShow) {
              if (
                this.favoriteArticles!.some((article) => article.id === item.id)
              ) {
                this.isFavorite.set(item.id!, true);
              }
            }
          });
        }
      });

    this.articlesService.searchArticles(''); // Afisăm inițial toate articolele
    this.articlesService.getFilteredArticles().subscribe((filteredArticles) => {
      this.totalPages = Math.ceil(
        (filteredArticles?.length || 1) / this.itemsPerPage
      );
      this.paginateArticles();
    });
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
