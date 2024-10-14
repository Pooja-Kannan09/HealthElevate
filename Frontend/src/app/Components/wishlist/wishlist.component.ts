import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { WishlistService } from '../../services/wishlist.service';
import { WishlistItem } from '../../models/wishlist-item.model';
import { HttpClientModule } from '@angular/common/http';
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, HttpClientModule, MatListModule, MatToolbarModule, DashboardComponent],
  templateUrl: './wishlist.component.html',
  styleUrl: './wishlist.component.css'
})
export class WishlistComponent implements OnInit {
  wishlistItems: WishlistItem[] = [];
  userId: string = ''; // Set to an empty string by default

  constructor(private wishlistService: WishlistService, private router: Router) {}

  ngOnInit(): void {
    const decodedUserId = this.getUserIdFromToken();
    this.userId = decodedUserId ? decodedUserId : ''; // Ensure userId is always a string

    if (this.userId) {
      this.loadWishlist();
    } else {
      console.error('No valid userId found in the token');
    }
  }

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

  // Function to load wishlist items for the user
  loadWishlist(): void {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    //console.log(this.userId)
    this.wishlistService.getWishlistItems(this.userId,headers).subscribe({
      next: (items) => this.wishlistItems = items,
      error: (err) => console.error('Item already in wishlist', err)
    });
  }

  // Function to remove an item from the wishlist
  removeItem(foodid: number): void {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    this.wishlistService.removeFromWishlist(foodid, this.userId,headers).subscribe({
      next: () => this.wishlistItems = this.wishlistItems.filter(item => item.foodid !== foodid),
      error: (err) => console.error('Failed to remove item', err)
    });
  }

  navigateToDashboard(){
    this.router.navigate(['/dashboard'])
  }

  onclickdash()
  {
    this.router.navigate(['/dashboard'])
  }
}
