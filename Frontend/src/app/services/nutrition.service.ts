import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WishlistItem } from '../models/wishlist-item.model';

// Interface for the nutrition data
export interface NutritionData {
  id: number;
  name: string;
  caloric: number;
  type: string;
  fat: number;
  carbon: number;
  protein: number;
  categoryId: number;
  image: string;
}

@Injectable({
  providedIn: 'root'
})
export class NutritionService {
   private apiUrl = 'http://localhost:5248/Nutrition'; // Update this URL based on your backend API
   //private baseUrl ='http://localhost:5024/api/Wishlist';
   //private apiUrl = 'http://localhost:5234/gateway/Nutrition';
  constructor(private http: HttpClient) { }

  // Method to fetch all nutrition data
  getNutritionData(headers: HttpHeaders): Observable<NutritionData[]> {
    return this.http.get<NutritionData[]>(this.apiUrl, {headers});
  }
  //  addToWishlist(item: WishlistItem): Observable<WishlistItem> {
  //    return this.http.post<WishlistItem>(this.baseUrl, item);
  //  }
 
}

  
