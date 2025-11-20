import { provideAnimations } from '@angular/platform-browser/animations';
import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { Basket, IBasket, IBasketItem } from '../models/basket';
import { Observable, tap } from 'rxjs';
import { ICatalog } from '../../store/models/products';

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
  addItemToBasket(product: ICatalog, quantity: number = 1) {
    const itemToAdd = this.mapProductToItemBasket(product);
    const basket = this.basket() ?? this.createBasket();
    basket.items = this.addOrUpdateItemBasket(basket.items, itemToAdd, quantity);
    return this.setBasket(basket);
  }
  private mapProductToItemBasket(product: ICatalog): IBasketItem {
    return {
      imageFile: product.imageFile,
      price: product.price,
      productId: product.id,
      productName: product.name,
      quantity: 0
    }
  }
  private createBasket(): Basket {
    let loginUser = 'alice';
    const basket = new Basket(loginUser);
    localStorage.setItem('basket_userName', loginUser);
    return basket;
  }
  private addOrUpdateItemBasket(items: IBasketItem[], newItem: IBasketItem, quantity: number) {
    const itemInBasket = items?.find(x => x.productId == newItem.productId);
    if (itemInBasket) {
      itemInBasket.quantity += quantity;
    } else {
      newItem.quantity = quantity;
      items.push(newItem);
    }
    return items;
  }
}
