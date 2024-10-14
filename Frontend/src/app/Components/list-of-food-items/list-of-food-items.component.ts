import { Component, OnInit, ViewChild } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NutritionData } from '../../services/nutrition.service';
import { NutritionService } from '../../services/nutrition.service';
import { WishlistService } from '../../services/wishlist.service';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { PageEvent } from '@angular/material/paginator';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpHeaders } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-list-of-food-items',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatProgressSpinnerModule, MatSnackBarModule, RouterModule, MatButtonModule, MatDividerModule, MatGridListModule, MatIconModule, MatToolbarModule, MatPaginatorModule],
  templateUrl: './list-of-food-items.component.html',
  styleUrls: ['./list-of-food-items.component.css']
})
export class ListOfFoodItemsComponent implements OnInit {
  hoverItem: any = null;
  meals: NutritionData[] = [];
  paginatedMeals: any[] = [];
  pageSize = 6; // Display 6 meals per page
  pageIndex = 0;
  loading = false; // Loading state
  searchQuery = ''; // For search input
  selectedCategory = ''; // For category filter

  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;

  constructor(private nutritionService: NutritionService, private wishlistService: WishlistService, private snackBar: MatSnackBar, private router: Router) {}

  ngOnInit(): void {
    this.fetchMeals();
  }

  fetchMeals(): void {
    this.loading = true; // Set loading to true
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    this.nutritionService.getNutritionData(headers).subscribe(
      (data: NutritionData[]) => {
        this.meals = data;
        this.paginatedMeals = this.meals.slice(0, this.pageSize);
        this.loading = false; // Set loading to false
      },
      (error) => {
        console.error('Error fetching meals:', error);
        this.loading = false; // Set loading to false
      }
    );
  }

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    const startIndex = this.pageIndex * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    this.paginatedMeals = this.meals.slice(startIndex, endIndex);
  }

  addToWishlist(meal: any): void {
    const favFoodDetails = {
      "favId": 0,
      "foodid": meal.id,
      "name": meal.name,
      "caloric": meal.caloric,
      "type": meal.type,
      "fat": meal.fat,
      "carbon": meal.carbon,
      "protein": meal.protein,
      "CategoryId": meal.categoryId,
      "image": meal.image,
      "UserId": this.getUserIdFromToken()
    };

    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    this.wishlistService.addToWishlist(favFoodDetails, headers).subscribe({
      next: (response: any) => {
        console.log('Meal added to wishlist:', response);
        this.snackBar.open('Added to Favorites!', 'Close', {
          duration: 3000,
          panelClass: ['snackbar-success']
        });
      },
      error: (error: any) => {
        console.error('Error adding to wishlist:', error);
        this.snackBar.open('Failed to add to wishlist!', 'Close', {
          duration: 3000,
          panelClass: ['snackbar-error']
        });
      }
    });
  }

  getUserIdFromToken(): string | null {
    const token = localStorage.getItem('token');
    if (!token) return null;

    try {
      const tokenPayload = JSON.parse(atob(token.split('.')[1]));
      return tokenPayload.userId ? tokenPayload.userId : null;
    } catch (error) {
      console.error("Error decoding token", error);
      return null;
    }
  }

  onclickdash() {
    this.router.navigate(['/dashboard']);
  }

  onSearch(event: Event) {
    const target = event.target as HTMLInputElement;
    this.searchQuery = target.value;

    this.filterMeals();
  }

  filterMeals() {
    let filteredMeals = this.meals;

    // Search filter
    if (this.searchQuery) {
      const lowerCaseQuery = this.searchQuery.toLowerCase();
      filteredMeals = filteredMeals.filter(meal =>
        meal.name.toLowerCase().includes(lowerCaseQuery) ||
        meal.caloric.toString() === this.searchQuery
      );
    }

    // Category filter
    if (this.selectedCategory) {
      filteredMeals = filteredMeals.filter(meal => meal.type === this.selectedCategory);
    }

    this.paginatedMeals = filteredMeals.slice(0, this.pageSize);

    if (this.paginatedMeals.length === 0) {
      this.snackBar.open('No meals found!', 'Close', {
        duration: 3000,
        panelClass: ['snackbar-warning']
      });
    }
  }

  resetFilters() {
    this.searchQuery = '';
    this.selectedCategory = '';
    this.paginatedMeals = this.meals.slice(0, this.pageSize);
  }

  onCategoryChange(event: any) {
    this.selectedCategory = event.value; // Update selected category
    this.filterMeals(); // Filter meals when category changes
  }
}
