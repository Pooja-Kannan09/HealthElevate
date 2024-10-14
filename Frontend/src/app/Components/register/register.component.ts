import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar,MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, MatCardModule,MatToolbarModule, MatFormFieldModule, MatInputModule,MatSnackBarModule,NgIf],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'] // Corrected to styleUrls
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router, private snackBar: MatSnackBar) {}

  register() {
    // Check if all fields are filled
    if (!this.username || !this.email || !this.password) {
      this.snackBar.open('Please fill in all the required fields!', 'Close', {
        duration: 3000,
        panelClass: ['snackbar-warning'] // Optional: Add a custom class for styling
      });
      return;
    }
  
    // Check password strength (basic example)
    const passwordStrength = this.checkPasswordStrength(this.password);
    if (!passwordStrength.isStrong) {
      this.snackBar.open(`Password is not strong enough! ${passwordStrength.message}`, 'Close', {
        duration: 3000,
        panelClass: ['snackbar-warning']
      });
      return;
    }
  
    // Call the register service if validation passes
    this.authService.register(this.username, this.email, this.password).subscribe(
      response => {
        // Handle successful registration
        this.router.navigate(['/login']);
        this.snackBar.open('Registration successful!', 'Close', { duration: 3000 });
      },
      error => {
        // Handle registration failure
        if (error.status === 409) { // Assuming 409 Conflict for existing user
          this.snackBar.open('User already present!', 'Close', { duration: 3000, panelClass: ['snackbar-warning'] });
        } else {
          this.snackBar.open('Registration failed!', 'Close', { duration: 3000 });
        }
      }
    );
  }
  
  // Method to check password strength
  checkPasswordStrength(password: string) {
    const minLength = 8;
    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasNumbers = /\d/.test(password);
    const hasSpecialChars = /[!@#$%^&*(),.?":{}|<>]/.test(password);
    
    const strengthMessages = [];
    if (password.length < minLength) strengthMessages.push(`Minimum length is ${minLength} characters.`);
    if (!hasUpperCase) strengthMessages.push('Password must contain at least one uppercase letter.');
    if (!hasLowerCase) strengthMessages.push('Password must contain at least one lowercase letter.');
    if (!hasNumbers) strengthMessages.push('Password must contain at least one number.');
    if (!hasSpecialChars) strengthMessages.push('Password must contain at least one special character.');
  
    return {
      isStrong: strengthMessages.length === 0,
      message: strengthMessages.join(' ')
    };
  }
  

  navigateToLogin(){
    this.router.navigate(["/login" ]);
  }

  onHome(){
    this.router.navigate(['/home']);
  }
}
