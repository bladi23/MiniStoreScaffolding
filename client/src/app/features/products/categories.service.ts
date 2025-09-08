
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Category {
  id: number;
  name: string;
}

@Injectable({ providedIn: 'root' })
export class CategoriesService {
  private http = inject(HttpClient);

  //URL ABSOLUTA al backend .NET (HTTPS)
  private apiBase = 'https://localhost:7027';

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.apiBase}/api/categories`);
  }
}
