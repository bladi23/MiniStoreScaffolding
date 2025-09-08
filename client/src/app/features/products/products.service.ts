import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  price: number;
  stock: number;
  categoryId: number;
  categoryName?: string | null; // puede venir null si no hay relación
}

export interface ProductCreate {
  name: string;
  price: number;
  stock: number;
  categoryId: number;
}

@Injectable({ providedIn: 'root' })
export class ProductsService {
  private http = inject(HttpClient);

  // URL ABSOLUTA a tu API .NET (evita el proxy en dev)
  private apiBase = 'https://localhost:7027';

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiBase}/api/products`);
  }

  createProduct(product: ProductCreate): Observable<Product> {
    return this.http.post<Product>(`${this.apiBase}/api/products`, product);
  }
}
