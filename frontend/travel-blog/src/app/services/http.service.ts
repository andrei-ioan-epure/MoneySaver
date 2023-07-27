import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Article, ServerArticle } from '../blog/model/article';
import { query } from '@angular/animations';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly endpoint = 'https://localhost:7207/api';
  constructor(private readonly http: HttpClient) {}

  getArticles(): Observable<Article[]> {
    const finalEndpoint = `${this.endpoint}/Article/get`;
    return this.http.get<Article[]>(finalEndpoint); //.pipe(map(serverArticles=>this.mapFromMultipleArticles(serverArticles)));
  }

  getArticle(id: number): Observable<Article> {
    const url = `${this.endpoint}/Article/get/${id}`;
    return this.http.get<Article>(url);
  }

  deleteArticle(id: number): void {
    const finalEndpoint = `${this.endpoint}/Article/delete/${id}`;

    this.http.delete(finalEndpoint).subscribe();
  }
  putArticle(id: number, body: Article): void {
    const finalEndpoint = `${this.endpoint}/Article/update/${id}`;
    this.http.put<Article>(finalEndpoint, body).subscribe();
  }
  /*private mapFromMultipleArticles(serverArticles:ServerArticle[]):Article[]
  {
    return serverArticles.map(serverArticle=>this.mapFromSingleArticle(serverArticle));
  }
  private mapFromSingleArticle(serverArticle:ServerArticle):Article{
    return{
      title:serverArticle.title,
      posted:serverArticle.posted,
      city:serverArticle.city,
      expiration:serverArticle.expiration,
      category:serverArticle.category,
      code:serverArticle.code,
      store:serverArticle.store,
      author:serverArticle.author,
      content:serverArticle.content,
      creatorId:serverArticle.creatorId,
    } as Article;
  }*/
}
