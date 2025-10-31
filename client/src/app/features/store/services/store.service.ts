import { HttpClient } from '@angular/common/http';
import { inject, Injectable, OnInit, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { ToastMessageService } from '../../../core/services/toastMessage.Service';
import { IPaginate } from '../../../shared/models/pagination';
import { IBrand, ICatalog, IType } from '../../../shared/models/products';
import { map, Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  private config = inject(APP_CONFIG); //injection in the angular
  toastMsg = inject(ToastMessageService);
  private http = inject(HttpClient);
  //#region signals
  products = signal<IPaginate<ICatalog> | null>(null);
  types = signal<IType[] | null>(null);
  brands = signal<IBrand[] | null>(null);
  //#endregion
  constructor() { }

  getAllProducts(): Observable<IPaginate<ICatalog>> {
    return this.http.get<IPaginate<ICatalog>>(`${this.config.baseUrl}/catalog`)
      .pipe(tap((data) => this.products.set(data)));
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
}
