import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { Basket, IBasket, IBasketItem } from '../models/basket';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { ICatalog } from '../../store/models/products';
import { IBasketTotal } from '../models/basketTotal';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  private config = inject(APP_CONFIG); //injection in the angular
  private http = inject(HttpClient);
  //Signal
  basket = signal<IBasket | null>(null);
  //Observable
  private basketTotalSource = new BehaviorSubject<IBasketTotal | null>(null);
  basketTotal$ = this.basketTotalSource.asObservable();
  //
  getBasket(userName: string): Observable<IBasket> {
    return this.http.get<IBasket>(`${this.config.baseUrl}/basket/getBasketByUserName/${userName}`)
      .pipe(
        tap((response) => {
          this.basket.set(response),
          //TODO
            this.calculateBasketTotal()
        }),
      );
  }
  setBasket(basket: IBasket) {
    return this.http.post<IBasket>(`${this.config.baseUrl}/basket/createBasket`, basket)
      .pipe(
        tap((response) => {
          this.basket.set(response),
          //TODO
            this.calculateBasketTotal()
        }),
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
    let loginUser = 'alice'; //TODO
    const basket = new Basket(loginUser);
    localStorage.setItem(this.config.basketUsername, loginUser);
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
  private calculateBasketTotal() {
    const basket = this.basket();
    if (!basket) return;

    const totalItems = basket.items.reduce((prev, item) => prev + item.price * item.quantity, 0);
    const discount = 0;
    const shippingTotal = 0;
    const tax = totalItems * 0.09;

    const totalToPay = (totalItems + shippingTotal + tax) - discount;
    this.basketTotalSource.next({
      discount,
      shippingTotal,
      tax,
      totalItems,
      totalToPay
    })
  }
}
