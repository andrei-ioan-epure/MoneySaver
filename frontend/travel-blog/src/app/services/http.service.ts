import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Article, ServerArticle } from '../blog/model/article';
import { query } from '@angular/animations';
import { User, ServerUser } from '../blog/model/user';
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
  getFilteredArticles(
    authors: string = 'All',
    category: string = 'All',
    city: string = 'All',
    store: string = 'All',
    posted: string = 'All',
    expiration: string = 'All'
  ): Observable<Article[]> {
    const finalEndpoint = `${this.endpoint}/Article/getFiltered`;
    const queryParams = {
      authors: authors == '' ? 'All' : authors,
      category: category,
      city: city,
      store: store,
      posted: posted,
      expiration: expiration,
    };
    return this.http
      .get<ServerArticle[]>(finalEndpoint, { params: queryParams })
      .pipe(
        map((serverArticles) => this.mapFromMultipleArticles(serverArticles))
      );
  }

  getFavoriteArticles(id: number): Observable<Article[]> {
    const finalEndpoint = `${this.endpoint}/User/getFavorite`;
    const queryParams = {
      id: id,
    };
    return this.http
      .get<ServerArticle[]>(finalEndpoint, { params: queryParams })
      .pipe(
        map((serverArticles) => this.mapFromMultipleArticles(serverArticles))
      );
  }

  addArticleToFavorites(userId: number, targetId: number): Observable<User> {
    const finalEndpoint = `${this.endpoint}/User/addFavorite`;
    const body = {
      userId: userId,
      targetId: targetId,
    };
    return this.http.put<any>(finalEndpoint, body);
  }

  deleteArticleFromFavorites(userId: number, targetId: number): void {
    const finalEndpoint = `${this.endpoint}/User/deleteFavorite`;
    const body = {
      userId: userId,
      targetId: targetId,
    };
    this.http.put(finalEndpoint, body).subscribe((s) => {
      console.log(s);
    });
  }

  private mapFromMultipleArticles(serverArticles: ServerArticle[]): Article[] {
    return serverArticles.map((serverArticle) =>
      this.mapFromSingleArticle(serverArticle)
    );
  }
  private mapFromSingleArticle(serverArticle: ServerArticle): Article {
    return {
      id: serverArticle.id,
      title: serverArticle.title,
      posted: serverArticle.posted,
      city: serverArticle.city,
      expiration: serverArticle.expiration,
      category: serverArticle.category,
      code: serverArticle.code,
      store: serverArticle.store,
      author: serverArticle.author,
      content: serverArticle.content,
      creatorId: serverArticle.creatorId,
    } as Article;
  }

  getUsers(): Observable<User[]> {
    const finalEndpoint = `${this.endpoint}/User/get`;
    return this.http
      .get<ServerUser[]>(finalEndpoint)
      .pipe(map((serverUsers) => this.mapFromMultipleUsers(serverUsers)));
  }

  private mapFromMultipleUsers(serverUsers: ServerUser[]): User[] {
    return serverUsers.map((serverUsers) =>
      this.mapFromSingleUser(serverUsers)
    );
  }
  private mapFromSingleUser(serverUser: ServerUser): User {
    return {
      userName: serverUser.userName,
      fullName: serverUser.fullName,
      email: serverUser.email,
      password: serverUser.password,
      isCreator: serverUser.isCreator,
    } as User;
  }

  postArticle(article: Article): Observable<Article> {
    const url = `${this.endpoint}/Article/add`;
    const body = article;
    return this.http.post<Article>(url, body);
  }
}
