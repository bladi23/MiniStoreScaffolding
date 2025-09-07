
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ProductsService, Product } from './products.service';
import { CategoriesService, Category } from './categories.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './products.html',
  styleUrls: ['./products.scss']
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];
  error = '';

  // Form fields
  form = {
    name: '',
    price: 0,
    stock: 0,
    categoryId: 0
  };
  formError = '';

  constructor(
    private productsService: ProductsService,
    private categoriesService: CategoriesService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.categoriesService.getCategories().subscribe({
      next: (data) => this.categories = data,
      error: () => this.error = 'No se obtuvieron las categorías.'
    });
  }

  loadProducts() {
    this.productsService.getProducts().subscribe({
      next: (data) => this.products = data,
      error: () => this.error = 'No se obtuvieron los productos.'
    });
  }

  addProduct() {
    this.formError = '';
    if (!this.form.name.trim() || this.form.price <= 0 || this.form.stock < 0 || !this.form.categoryId) {
      this.formError = 'Todos los campos son obligatorios y deben ser válidos.';
      return;
    }
    this.productsService.createProduct(this.form).subscribe({
      next: () => {
        this.loadProducts();
        this.form = { name: '', price: 0, stock: 0, categoryId: 0 };
      },
      error: () => this.formError = 'No se pudo agregar el producto.'
    });
  }
}
