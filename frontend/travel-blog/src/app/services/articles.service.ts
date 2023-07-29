import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Articles } from '../blog/model/article';

@Injectable({
  providedIn: 'root',
})
export class ArticlesService {
  private apiUrl = 'https://api.example.com/articles';
  private articles: Articles = [];
  private searchTerm: string = '';
  private filteredArticles: BehaviorSubject<Articles> =
    new BehaviorSubject<Articles>([]);

  constructor(private http: HttpClient) {}

  getArticles(): Observable<Articles> {
    return this.http.get<Articles>(this.apiUrl);
  }

  setArticles(articles: Articles) {
    this.articles = articles;
    this.filterArticles();
  }

  setSearchTerm(term: string) {
    this.searchTerm = term;
    if (term != null) this.filterArticles();
  }

  private filterArticles() {
    this.filteredArticles.next(
      this.articles.filter((article) => {
        return (
          article.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
          article.content
            .toLowerCase()
            .includes(this.searchTerm.toLowerCase()) ||
          article.category
            .toLowerCase()
            .includes(this.searchTerm.toLowerCase()) ||
          article.city.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
          article.author.toLowerCase().includes(this.searchTerm.toLowerCase())
        );
      })
    );
  }

  getFilteredArticles(): Observable<Articles> {
    return this.filteredArticles.asObservable();
  }

  searchArticles(searchTerm: string) {
    this.setSearchTerm(searchTerm);
  }
}
