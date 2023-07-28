import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginResponse, UserLogin } from '../blog/model/user';
import { BehaviorSubject, Observable, Subject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly endpoint = 'https://localhost:7207/api';

  private userToken: string | null = null;
  private isLoggedSubject = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private readonly http: HttpClient) {}

  LogIn(userLogin: UserLogin): Observable<LoginResponse> {
    const finalEndpoint = `${this.endpoint}/User/login`;
    const body = userLogin;
    return this.http.put<LoginResponse>(finalEndpoint, body).pipe(
      tap((res: LoginResponse) => {
        this.setToken(res.token);
      })
    );
  }

  getToken() {
    if (!this.userToken) this.userToken = localStorage.getItem('jwt');
    return this.userToken;
  }

  setToken(token: string) {
    localStorage.setItem('jwt', token);
    this.userToken = token;
    this.isLoggedSubject.next(true);
  }

  logOut():void{
    localStorage.removeItem('jwt');
    this.isLoggedSubject.next(false);
  }

  isLoggedIn(): Observable<boolean>{
    return this.isLoggedSubject.asObservable();
  }

  private hasToken():boolean{
    return !!localStorage.getItem('jwt');
  }
}
