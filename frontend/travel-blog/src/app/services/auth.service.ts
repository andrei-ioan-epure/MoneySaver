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
  private userId: number | null = null;
  private userRole: string | null = null;
  private userFullName : string | null = null;
  private isLoggedSubject = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private readonly http: HttpClient) {}

  LogIn(userLogin: UserLogin): Observable<LoginResponse> {
    const finalEndpoint = `${this.endpoint}/User/login`;
    const body = userLogin;
    return this.http.put<LoginResponse>(finalEndpoint, body).pipe(
      tap((res: LoginResponse) => {
        this.setLoginResponse(res);
      })
    );
  }

  getToken() {
    if (!this.userToken) this.userToken = localStorage.getItem('jwt');
    return this.userToken;
  }

  getRole(){
    if (!this.userRole) this.userRole = localStorage.getItem('userRole');
    return this.userRole;
  }

  getId(){
    if (!this.userId) {
      let userId = localStorage.getItem('userId');
      this.userId = userId !== null? parseInt(userId) : null;
    }
    return this.userId;
  }

  getFullName(){
    if (!this.userFullName) this.userFullName = localStorage.getItem('userFullName');
    return this.userFullName;
  }

  setLoginResponse(response: LoginResponse) {
    localStorage.setItem('jwt', response.token);
    localStorage.setItem('userId', response.id.toString());
    localStorage.setItem('userRole',response.role);
    localStorage.setItem('userFullName', response.fullName);
    this.userToken = response.token;
    this.userId = response.id;
    this.userRole = response.role;
    this.userFullName = response.fullName;
    this.isLoggedSubject.next(true);
  }

  logOut():void{
    localStorage.removeItem('jwt');
    localStorage.removeItem('userId');
    localStorage.removeItem('userRole');
    this.isLoggedSubject.next(false);
  }

  isLoggedIn(): Observable<boolean>{
    return this.isLoggedSubject.asObservable();
  }

  hasToken():boolean{
    return !!localStorage.getItem('jwt');
  }
}
