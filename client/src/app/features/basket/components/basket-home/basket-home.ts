import { Component, inject } from '@angular/core';
import { BasketService } from '../../services/basket.service';
import { DecimalPipe } from '@angular/common';
import { BasketTotalComponent } from "../basket-total/basket-total.component";
import { IBasketItem } from '../../models/basket';

@Component({
  selector: 'app-basket-home',
  imports: [DecimalPipe, BasketTotalComponent],
  templateUrl: './basket-home.html',
  styleUrl: './basket-home.css'
})
export class BasketHome {
  basketService = inject(BasketService);
  //
  increaseItemQuantity(item: IBasketItem) {
    this.basketService.increaseItemQuantity(item).subscribe();
  }
  decreaseItemQuantity(item: IBasketItem) {
    this.basketService.decreaseItemQuantity(item).subscribe();
  }
  removeItemFromBasket(productId: string) {
    this.basketService.removeItemFromBasket(productId).subscribe(res => {
      console.log("🚀 ~ BasketHome ~ removeItemFromBasket ~ res:", res)
    });
  }
  deleteBasket() {
    const basket = this.basketService.basket();
    if (basket) {
      this.basketService.deleteBasket(basket.userName).subscribe(res => {
        console.log("🚀 ~ BasketHome ~ deleteBasket ~ res:", res)
      });
    }
  }
}
