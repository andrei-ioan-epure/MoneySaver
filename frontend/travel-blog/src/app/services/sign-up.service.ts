import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Article } from '../blog/model/article';
import { User } from '../blog/model/user';

@Injectable({
  providedIn: 'root'
})
export class SignUpService {
  private readonly endpoint="https://localhost:7207/api";

  constructor(private readonly http:HttpClient ) { }

  postUser(user: User):Observable<User>{
    const finalEndpoint=`${this.endpoint}/User/add`;
    const body = user;
    return this.http.post<User>(finalEndpoint, body);
  }
}
