import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {
  private apiURL: string = 'https://localhost:7207';

  constructor(private httpClient: HttpClient) { }

  getArticles(): Observable<any> {
    return this.httpClient.get(`${this.apiURL}/get`);
  }
}
