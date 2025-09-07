import { Routes } from '@angular/router';
import { ProductsComponent } from './features/products/products.component'; // <— IMPORT DIRECTO

export const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: '**', redirectTo: 'products' }
];
