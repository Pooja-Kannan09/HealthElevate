import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../app/services/auth.service';
import { MatSnackBar } from '@angular/material/snack-bar';

export const authGuardGuard: CanActivateFn = (route, state) => {
  const service = inject(AuthService)
  const router = inject(Router)
  const snackBar = inject(MatSnackBar);
  if (service.isLoggedIn() === true) {
    return true;
  } else {
    console.log("Not a user, back to login");
    // Display the snackbar notification
    snackBar.open('You need to login to access this page.', 'OK', {
      duration: 3000, // Snackbar will be visible for 3 seconds
    });
    router.navigate(['/home']);
    return false;
  }
  
};
