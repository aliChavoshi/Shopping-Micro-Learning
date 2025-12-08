import { HttpClient } from '@angular/common/http';
import { computed, effect, inject, Injectable, signal } from '@angular/core';
import { APP_CONFIG } from '../../../core/config/appConfig.token';
import { Basket, IBasket, IBasketItem } from '../models/basket';
import { EMPTY, Observable, tap } from 'rxjs';
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
  // basketTotalSource = signal<IBasketTotal | null>(null);
  basketTotalSource = computed<IBasketTotal | null>(() => {
    const basket = this.basket(); //signal
    if (!basket) return null;

    const totalItems = basket.items.reduce((prev, item) => prev + item.price * item.quantity, 0);
    const discount = 0;
    const shippingTotal = 0;
    const tax = totalItems * 0.09;

    const totalToPay = (totalItems + shippingTotal + tax) - discount;
    return {
      discount,
      shippingTotal,
      tax,
      totalItems,
      totalToPay
    }
  });
  //
  constructor() {
    // effect(() => {
    //   const b = this.basket();
    //   if (b) {
    //     this.calculateBasketTotal();
    //   }
    // });
  }
  getBasket(userName: string): Observable<IBasket> {
    return this.http.get<IBasket>(`${this.config.baseUrl}/basket/getBasketByUserName/${userName}`)
      .pipe(
        tap((response) =>
          this.basket.set(response)
        ),
      );
  }
  setBasket(basket: IBasket) {
    return this.http.post<IBasket>(`${this.config.baseUrl}/basket/createBasket`, basket)
      .pipe(
        tap((response) =>
          this.basket.set(response)
        ),
      )
  }
  addItemToBasket(product: ICatalog, quantity: number = 1) {
    const itemToAdd = this.mapProductToItemBasket(product);
    const basket = this.basket() ?? this.createBasket();
    basket.items = this.addOrUpdateItemBasket(basket.items, itemToAdd, quantity);
    return this.setBasket(basket);
  }
  increaseItemQuantity(item: IBasketItem): Observable<IBasket> {
    const basket = this.basket();
    if (!basket) return EMPTY;
    const index = basket.items.findIndex(x => x.productId === item.productId);
    if (index > 0) {
      basket.items[index].quantity++;
      return this.setBasket(basket);
    } else {
      const product = this.mapItemBasketToProduct(item);
      return this.addItemToBasket(product, 1);
    }
  }
  decreaseItemQuantity(item: IBasketItem) {
    //Basket
    //Any
    //Index
    //OK => quantity --
    // Not OK => Delete
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
  private mapItemBasketToProduct(item: IBasketItem): ICatalog {
    return {
      brands: {
        id: '',
        name: ''
      },
      description: '',
      id: item.productId,
      imageFile: item.imageFile,
      name: item.productName,
      price: item.price,
      summary: '',
      types: {
        id: '',
        name: ''
      }
    }
  }
  // private calculateBasketTotal() {
  //   const basket = this.basket(); //signal
  //   if (!basket) return;

  //   const totalItems = basket.items.reduce((prev, item) => prev + item.price * item.quantity, 0);
  //   const discount = 0;
  //   const shippingTotal = 0;
  //   const tax = totalItems * 0.09;

  //   const totalToPay = (totalItems + shippingTotal + tax) - discount;
  //   this.basketTotalSource.set({
  //     discount,
  //     shippingTotal,
  //     tax,
  //     totalItems,
  //     totalToPay
  //   })
  // }
}
