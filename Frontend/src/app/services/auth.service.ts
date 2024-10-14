import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  //private apiUrl = 'http://localhost:5082/api/Auth'; 
  private apiUrl = 'http://localhost:5248/Auth';
  constructor(private http: HttpClient, private router: Router) {}

  register(username: string, email: string, password: string) {
    const payload = { username, email, password };
    return this.http.post(`${this.apiUrl}/register`, payload);
  }

  login(username: string, password: string) {
    const payload = { username, password };
    return this.http.post<{ token: string }>(`${this.apiUrl}/login`, payload);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
 
  
  isLoggedIn(): boolean{
    if(localStorage.getItem('token')== null){
    return false;}
    else{
      return true;
    }
  }
}
