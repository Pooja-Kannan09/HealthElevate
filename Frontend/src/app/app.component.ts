import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ListOfFoodItemsComponent } from './Components/list-of-food-items/list-of-food-items.component';
import { WishlistComponent } from './Components/wishlist/wishlist.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { SearchComponent } from './Components/search/search.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { HomePageComponent } from './Components/home/home-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,ListOfFoodItemsComponent,WishlistComponent,DashboardComponent,HomePageComponent,
    LoginComponent,
    RegisterComponent,
    SearchComponent,
    MatToolbarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
  MatPaginatorModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'NutritionApp';
}
