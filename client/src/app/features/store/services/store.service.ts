import { HttpClient } from '@angular/common/http';
import { inject, Injectable, OnInit, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { ToastMessageService } from '../../../core/services/toastMessage.Service';
import { IPaginate } from '../../../shared/models/pagination';
import { ICatalog } from '../../../shared/models/products';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  private config = inject(APP_CONFIG); //injection in the angular
  toastMsg = inject(ToastMessageService);
  private http = inject(HttpClient);
  //Signals
  products = signal<IPaginate<ICatalog> | null>(null);
  constructor() { }

  getAllProducts(): Observable<IPaginate<ICatalog>> {
    return this.http.get<IPaginate<ICatalog>>(`${this.config.baseUrl}/catalog`)
      .pipe(tap((data) => this.products.set(data)));
  }

}
