import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import {MatIconModule} from '@angular/material/icon';
import { Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MatToolbarModule,MatCardModule,MatIconModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  constructor(private router: Router) {}
  //Navigate to search nutrition page
  navigateToSearchNutrition() {
    if(localStorage.getItem('token')){
      this.router.navigate(['/search-nutrition']);
    }
    else{
      this.router.navigate(['/home'])
    }
  }
  //Navigate to wishlist page
  navigateToWishlist(){
    if(localStorage.getItem('token')){
      this.router.navigate(['/wishlist']);
    }
    else{
      this.router.navigate(['/home'])
    }
  }
  //Navigate to list of foods page
  navigateListofFoods(){
    if(localStorage.getItem('token')){
      this.router.navigate(['/list-of-food-items']);
    }
    else{
      this.router.navigate(['/home'])
    }
    
  }
  // Logout function
  onLogout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/home']);
  }
}
