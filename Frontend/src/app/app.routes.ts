import { Routes } from '@angular/router';
import { WishlistComponent } from './Components/wishlist/wishlist.component';
import { ListOfFoodItemsComponent } from './Components/list-of-food-items/list-of-food-items.component';
import { LoginComponent } from './Components/login/login.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { SearchComponent } from './Components/search/search.component';
import { RegisterComponent } from './Components/register/register.component';
import { HomePageComponent } from './Components/home/home-page.component';
import { authGuardGuard } from '../Guard/auth-guard.guard';

export const routes: Routes = [
  //Unprotected Routes
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomePageComponent},
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },

  //Protected routes
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [authGuardGuard],  // Protect the dashboard route
  },
  {
    path: 'search-nutrition',
    component: SearchComponent,
    canActivate: [authGuardGuard],  // Protect the search nutrition route
  },
  {
    path: 'list-of-food-items',
    component: ListOfFoodItemsComponent,
    canActivate: [authGuardGuard],  // Protect the list of foods route
  },
  {
    path: 'wishlist',
    component: WishlistComponent,
    canActivate: [authGuardGuard],  // Protect the wishlist route
  },
  


];
