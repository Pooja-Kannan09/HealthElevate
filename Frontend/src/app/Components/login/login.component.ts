import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    MatCardModule, 
    MatFormFieldModule, 
    MatInputModule, 
    FormsModule, 
    MatSnackBarModule,
    MatToolbarModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],

})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router, private snackBar: MatSnackBar) {}

  login() {
    if (!this.username || !this.password) {
      this.snackBar.open('Please fill in all the required fields!', 'Close', {
        duration: 3000,
        panelClass: ['snackbar-warning'] // Optional: Add a custom class for styling
      });
      return;
    }
  
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        this.snackBar.open('Login successful!', 'Close', { duration: 3000 });
        this.router.navigate(['/dashboard']);
      },
      error: (errorResponse) => {
        // Check the error status for invalid credentials
        if (errorResponse.status === 401) {
          this.snackBar.open('Invalid credentials! Please try again.', 'Close', { duration: 3000 });
        } else {
          this.snackBar.open('Login failed! Please try again later.', 'Close', { duration: 3000 });
        }
      }
    });
  }
  onHome(){
    this.router.navigate(['/home']);
  }
}
