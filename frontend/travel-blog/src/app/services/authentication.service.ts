// authentication.service.ts

import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private isLogged = false;

  constructor() { }

  getIsLogged(): boolean {
    return this.isLogged;
  }

  setIsLogged(value: boolean): void {
    this.isLogged = value;
  }
}
