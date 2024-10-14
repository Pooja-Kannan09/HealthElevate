import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MatCardActions, MatCardContent, MatCardHeader, MatCardModule, MatCardTitle } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { animate, style, transition, trigger } from '@angular/animations';
import { MatIcon } from '@angular/material/icon';
import { MatToolbar, MatToolbarModule } from '@angular/material/toolbar';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatToolbarModule,
    FormsModule,
    MatCardActions,
    MatCardContent,
    MatCardTitle,
    MatCardHeader,
    MatCardActions,
    MatFormFieldModule,
    MatIcon,
    MatInputModule,
    MatSnackBarModule
  ],
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('500ms', style({ opacity: 1 }))
      ])
    ])
  ]
})
export class HomePageComponent {
  searchQuery: string = ''; // Holds the user input for activity
  foodQuery: string = ''; // Holds the user input for nutrient search
  activities: any[] = []; // Stores the activities fetched from the API
  nutrientInfo: any[] = []; // Stores the nutrients fetched from API
  isLoading: boolean = false; // Loader indicator
  activitySearchPerformed: boolean = false; // Tracks if an activity search has been performed
  nutrientSearchPerformed: boolean = false; // Tracks if a nutrient search has been performed
  searchPerformed: boolean = false; // Add this flag to track if any search is done

  apiUrl = 'https://api.api-ninjas.com/v1/caloriesburned?activity='; // API Endpoint
  apiKey = 'Rh/UEfeiPZz/NN7JYapj8w==408yGJLkIx68tzIK'; // Replace with your API key

  // API settings for Nutritionix nutrient breakdown
  nutritionApiUrl = 'https://trackapi.nutritionix.com/v2/natural/nutrients';
  nutritionAppId = '32a6e539'; // Replace with your x-app-id
  nutritionAppKey = '89cd88957f19cdd169a1e398bbc194c4'; // Replace with your x-app-key

  constructor(private router: Router, private http: HttpClient) {}

  // Navigate to login page
  navigateToLogin() {
    this.router.navigate(['/login']);
  }

  // Navigate to register page
  navigateToRegister() {
    this.router.navigate(['/register']);
  }

  // Method to search for activity
  searchActivity() {
    this.clearActivityResults(); // Clear previous activity results
    this.isLoading = true; // Show loader
    this.activitySearchPerformed = true; // Reset flag to true at the start
    this.searchPerformed = true; // Set flag to true once search is performed
    if (!this.searchQuery) {
      this.isLoading = false; // Hide loader if no query
      return; // Exit if no query
    }

    const headers = new HttpHeaders({
      'X-Api-Key': this.apiKey
    });

    this.http.get<any[]>(`${this.apiUrl}${this.searchQuery}`, { headers })
      .subscribe(
        (response) => {
          this.activities = response; // Store the response data
          console.log('Activities fetched:', this.activities); // Debug log
          this.isLoading = false; // Hide loader after results
        },
        (error) => {
          console.error('Error fetching activity data', error);
          this.isLoading = false; // Hide loader on error
        }
      );
  }

  // Method to search for nutrients
  searchNutrients() {
    this.clearNutrientResults(); // Clear previous nutrient results
    this.isLoading = true; // Show loader
    this.nutrientSearchPerformed = true; // Reset flag to true at the start
    this.searchPerformed = true; // Set flag to true once search is performed
    
    if (!this.foodQuery) {
      this.isLoading = false; // Hide loader if no food query
      return; // Exit if no food query
    }

    const headers = new HttpHeaders({
      'x-app-id': this.nutritionAppId,
      'x-app-key': this.nutritionAppKey,
      'Content-Type': 'application/json',
    });

    const body = { query: this.foodQuery };

    this.http.post<any>(this.nutritionApiUrl, body, { headers })
      .subscribe(
        (response) => {
          this.nutrientInfo = response.foods; // Store the response data
          console.log('Nutrient info fetched:', this.nutrientInfo); // Debug log
          this.isLoading = false; // Hide loader after results
        },
        (error) => {
          console.error('Error fetching nutrient data', error);
          this.isLoading = false; // Hide loader on error
        }
      );
  }

  clearAll() {
    // Clear search input fields
    this.searchQuery = '';
    this.foodQuery = '';
  
    // Reset activities and nutrient information
    this.activities = [];
    this.nutrientInfo = [];
  
    // Reset the search performed flags
    this.activitySearchPerformed = false;
    this.nutrientSearchPerformed = false;
  
    // Stop the loader if it's running
    this.isLoading = false;
  }
  // Method to clear results for activities
  clearActivityResults() {
    this.activities = [];
  }

  // Method to clear results for nutrients
  clearNutrientResults() {
    this.nutrientInfo = [];
  }

  // Method to clear search query and reset flags
  clearActivityInput() {
    this.searchQuery = ''; // Clear the input field
    this.clearActivityResults(); // Clear previous results
    this.isLoading = false; // Hide loader
    this.activitySearchPerformed = false; // Reset search performed flag
  }

  // Method to clear nutrient input and reset flags
  clearNutrientInput() {
    this.foodQuery = ''; // Clear the input field
    this.clearNutrientResults(); // Clear previous results
    this.isLoading = false; // Hide loader
    this.nutrientSearchPerformed = false; // Reset search performed flag
  }
}
