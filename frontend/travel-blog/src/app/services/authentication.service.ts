import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private isLogged = false;

  constructor(private http: HttpClient) {}

  getIsLogged(): boolean {
    return this.isLogged;
  }

  setIsLogged(value: boolean): void {
    this.isLogged = value;
  }

  login(email: string, password: string): Observable<boolean> {
    const userLoginDto = { email, password };

    return this.http.post<any>(`https://localhost:7207/api/User/login`, userLoginDto).pipe(
      map(response => {
        this.setIsLogged(true);
        return true;
      }),
      catchError(error => {
        console.error('Login failed:', error);
        this.setIsLogged(false);
        return of(false);
      })
    );
  }

  logout(): void {
    this.setIsLogged(false);
  }
}
