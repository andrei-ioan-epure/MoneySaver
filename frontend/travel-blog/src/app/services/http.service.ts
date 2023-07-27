import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Article, ServerArticle } from '../blog/model/article';
import { User, ServerUser } from '../blog/model/user';
@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly endpoint = 'https://localhost:7207/api';
  constructor(private readonly http: HttpClient) {}

  getArticles(): Observable<Article[]> {
    const finalEndpoint = `${this.endpoint}/Article/get`;
    return this.http
      .get<ServerArticle[]>(finalEndpoint)
      .pipe(
        map((serverArticles) => this.mapFromMultipleArticles(serverArticles))
      );
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

  private mapFromMultipleArticles(serverArticles: ServerArticle[]): Article[] {
    return serverArticles.map((serverArticle) =>
      this.mapFromSingleArticle(serverArticle)
    );
  }
  private mapFromSingleArticle(serverArticle: ServerArticle): Article {
    return {
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

  deleteArticle(queryParams: any): void {
    const finalEndpoint = `${this.endpoint}/Article/delete`;
    this.http.delete(finalEndpoint, { params: queryParams }).subscribe((s) => {
      console.log(s);
    });
  }
}
