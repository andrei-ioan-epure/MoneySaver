import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { inject } from '@angular/core';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService: AuthService = inject(AuthService);
  const router : Router = inject(Router);

  if(authService.getRole() != "admin")
  {
    router.navigate(['/signin']);
    return false;
  }
  return true;
};
