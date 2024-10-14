import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WishlistItem } from '../models/wishlist-item.model';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class WishlistService {

  private baseUrl = 'http://localhost:5248/Wishlist';

  constructor(private http: HttpClient) {}

  getWishlistItems(userId: string, headers: HttpHeaders): Observable<WishlistItem[]> {
    return this.http.get<WishlistItem[]>(`${this.baseUrl}/${userId}`,{headers});
  }
 
  addToWishlist(wishlistItem: any, headers: HttpHeaders): Observable<any> {
    const url = 'http://localhost:5248/Wishlistadd';
    return this.http.post(url, wishlistItem, {headers});
  }
  

  removeFromWishlist(itemId: number, userId: string, headers:HttpHeaders): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${itemId}/${userId}`,{headers});
  }
}
