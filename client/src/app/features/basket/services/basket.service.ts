import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { IBasket } from '../models/basket';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private config = inject(APP_CONFIG); //injection in the angular
  private http = inject(HttpClient);
  //
  basket = signal<IBasket | null>(null);
  constructor() { }

  getBasket(userName: string): Observable<IBasket> {
    return this.http.get<IBasket>(`${this.config.baseUrl}/basket/getBasketByUserName/${userName}`)
      .pipe(
        tap((response) => this.basket.set(response))
      );
  }
  setBasket(basket: IBasket) {
    return this.http.post<IBasket>(`${this.config.baseUrl}/basket/createBasket`, basket)
      .pipe(
        tap((response) => this.basket.set(response))
      )
  }
}
