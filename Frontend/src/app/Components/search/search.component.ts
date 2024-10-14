import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { MatCardModule } from '@angular/material/card'; // For Angular Material cards
import { MatButtonModule } from '@angular/material/button'; // For Angular Material buttons
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; // For loader
import { HttpHeaders } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { MatToolbar,MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css'],
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    MatCardModule, 
    MatButtonModule, 
    MatProgressSpinnerModule,
    RouterModule,
    MatToolbarModule
  ] // Add Angular Material modules
})
export class SearchComponent {
  title: string = '';
  nutritionData: any = null;
  errorMessage: string = '';
  isLoading: boolean = false;
  searchInitiated: boolean = false;
  
  constructor(private http: HttpClient, private router: Router) {}
// Function to decode the JWT token and extract the userId
getUserIdFromToken(): string | null {
  const token = localStorage.getItem('token'); // Or sessionStorage if you store the token there
  if (!token) return null;

  try {
    const tokenPayload = JSON.parse(atob(token.split('.')[1]));
    return tokenPayload.userId ? tokenPayload.userId : null;
  } catch (error) {
    console.error("Error decoding token", error);
    return null;
  }
}
  searchNutrition() {
    if (!this.title) {
      this.nutritionData = null; // Reset nutrition data if title is empty
      this.searchInitiated = false; // Reset search initiated status
      return;
    }
    if (this.title) {
      this.isLoading = true;
      this.errorMessage = '';
      this.nutritionData = null;
      this.searchInitiated = true; 
      const token = localStorage.getItem('token');
      const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      console.log(this.title);
      // this.http.get(`${baseUrl}?title=${this.title}`)
      this.http.get( `http://localhost:5248/Nutritionsearch?title=${this.title}`, {headers})
        .subscribe({
          next: (data) => {
            this.nutritionData = data;
            this.isLoading = false;
          },
          error: (error) => {
            console.error('Error fetching nutrition data', error);
            this.nutritionData = null
            //this.errorMessage = `No data found for ${this.title}. Please try again later.`;
            this.isLoading = false;
          }
        });
    } else {
      this.errorMessage = 'Please enter a dish name.';
    }
  }
  onclickdash()
  {
    this.router.navigate(['/dashboard'])
  }

  onInputChange() {
    if (!this.title) {
      this.nutritionData = null; // Reset nutrition data if input is cleared
      this.searchInitiated = false; // Reset search initiated status
    }
  }
}
