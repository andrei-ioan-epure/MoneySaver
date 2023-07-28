import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../blog/model/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly endpoint="https://localhost:7207/api";

  constructor(private readonly http:HttpClient) { }

  LogIn(userLogin:User){
    const finalEndpoint = `${this.endpoint}/User/login`;
    const body = userLogin;
    return this.http.put(finalEndpoint, body);
  }
}
