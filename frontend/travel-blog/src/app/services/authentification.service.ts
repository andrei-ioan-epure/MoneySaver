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

  logout(): void {
    this.setIsLogged(false);
  }
}