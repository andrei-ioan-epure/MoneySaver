import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Articles } from '../model/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { Router } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { AuthService } from 'src/app/services/auth.service';
import { Observable, Subject, Subscriber, Subscription, elementAt } from 'rxjs';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss'],
})
export class ArticleComponent implements OnInit, OnDestroy {
  @Input() articles?: Articles; // Adăugăm Input decorator pentru a primi articolele din componenta părinte
  @Input() isAdmin: boolean = false;
  url?: string;
  isFavouritesPage?: boolean = false;
  isFavorite: Map<number, boolean> = new Map<number, boolean>();
  favoriteArticles?: Articles = [];
  articlesToShow?: Articles = [];
  itemsPerPage: number = 6;
  currentPage: number = 1;
  totalPages: number = 1;
  public isLoggedIn: Observable<boolean>;
  private subscription!: Subscription;

  constructor(
    private readonly articlesService: ArticlesService,
    private readonly router: Router,
    private readonly httpService: HttpService,
    private readonly authService: AuthService
  ) {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  ngOnDestroy(): void {
    this.url = this.router.url;
    if (!this.url.includes('favourites') && this.subscription != null)
      this.subscription.unsubscribe();
  }

  ngOnInit() {
    if (!this.authService.hasToken()) {
      this.httpService.getArticles().subscribe((data) => {
        this.articlesToShow = data;
        this.articlesService.setArticles(data);
      });
    } else {
      if (this.authService.getRole() == 'admin') {
        this.isAdmin = true;
      }
      this.httpService
        .getFavoriteArticles(this.authService.getId() as number)
        .subscribe((res) => {
          this.favoriteArticles = res;

          this.url = this.router.url;
          if (!this.url.includes('favourites')) {
            this.httpService.getArticles().subscribe((res) => {
              this.articlesToShow = res.sort((a, b) =>
                a.posted < b.posted ? 1 : -1
              );
              this.articlesService.setArticles(res);
              for (var item of this.articlesToShow) {
                if (
                  this.favoriteArticles!.some(
                    (article) => article.id === item.id
                  )
                ) {
                  this.isFavorite.set(item.id!, true);
                }
              }
            });
          } else {
            this.subscription = this.httpService.favoritesObserver.subscribe(
              (res) => {
                this.articlesToShow = res.sort((a, b) =>
                  a.posted < b.posted ? 1 : -1
                );
                this.articlesService.setArticles(res);

                this.isFavouritesPage = true;
              }
            );
          }
        });
    }

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
    //console.log('Articles received from Child:', articlesReceived);
    this.httpService.emptyObserverAndSet(articlesReceived);
    
    this.articlesToShow = articlesReceived.sort((a, b) =>
      a.posted < b.posted ? 1 : -1
    );
    this.articlesService.setArticles(this.articlesToShow);
  }

  private paginateArticles() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.articlesService.getFilteredArticles().subscribe((filteredArticles) => {
      for (var item of filteredArticles) {
        if (this.favoriteArticles!.some((article) => article.id === item.id)) {
          this.isFavorite.set(item.id!, true);
        }
      }
      this.articlesToShow = filteredArticles
        ?.sort((a, b) => (a.posted < b.posted ? 1 : -1))
        .slice(startIndex, endIndex);
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
