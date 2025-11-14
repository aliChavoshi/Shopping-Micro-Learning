import { HttpClient, HttpParams } from '@angular/common/http';
import { effect, inject, Injectable, OnInit, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { ToastMessageService } from '../../../core/services/toastMessage.Service';
import { IPaginate } from '../../../shared/models/pagination';
import { IBrand, ICatalog, IType } from '../models/products';
import { from, map, Observable, of, scheduled, tap } from 'rxjs';
import { sign } from 'crypto';
import { ProductParams } from '../models/productParams';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  private config = inject(APP_CONFIG); //injection in the angular
  private http = inject(HttpClient);
  //#region signals
  products = signal<IPaginate<ICatalog> | null>(null);
  types = signal<IType[] | null>(null);
  brands = signal<IBrand[] | null>(null);
  params = signal<ProductParams>(new ProductParams());
  //#endregion
  constructor() {
    effect(() => {
      const _ = this.params();
      this.getAllProducts().subscribe();
    });
  }

  getAllProducts(): Observable<IPaginate<ICatalog>> {
    let params = this.generateProductsParams();
    return this.http.get<IPaginate<ICatalog>>(`${this.config.baseUrl}/catalog`, { params })
      .pipe(
        tap((data) => this.products.set(data))
      );
  }
  getAllTypes() {
    return this.http.get<IType[]>(`${this.config.baseUrl}/catalog/getAllTypes`)
      .pipe(
        map((x) => [{ id: '', name: 'All' }, ...x]),
        tap(data => this.types.set(data))
      );
  }
  getAllBrands() {
    return this.http.get<IBrand[]>(`${this.config.baseUrl}/catalog/getAllBrands`)
      .pipe(
        map((x) => [{ id: '', name: 'All' }, ...x]),
        tap(data => this.brands.set(data))
      );
  }
  getProductById(id: string): Observable<ICatalog> {
    const products = this.products();
    if (products) {
      const product = products.data.find(x => x.id === id);
      if (product) return of(product);
    }
    return this.http.get<ICatalog>(`${this.config.baseUrl}/catalog/GetProductById/${id}`);
  }
  setParams(parameters: ProductParams) {
    this.params.set(parameters);
  }
  private generateProductsParams() {
    let params = new HttpParams();
    let productParams = this.params();
    if (productParams?.typeId) {
      params = params.append('typeId', productParams.typeId);
    }
    if (productParams?.brandId) {
      params = params.append('brandId', productParams.brandId);
    }
    if (productParams?.search) {
      params = params.append('search', productParams.search);
    }
    if (productParams?.sort == 'priceAsc' || productParams?.sort == 'priceDesc') {
      params = params.append('sort', productParams.sort);
    }
    params = params.append('pageIndex', productParams?.pageIndex ?? 1);
    params = params.append('pageSize', productParams?.pageSize ?? 9);

    return params;
  }
}
