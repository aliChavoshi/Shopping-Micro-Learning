import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { StoreService } from '../../services/store.service';
import { DecimalPipe } from '@angular/common';
import { BasketService } from '../../../basket/services/basket.service';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  imports: [RouterLink, DecimalPipe]
})
export class ProductDetailComponent implements OnInit {
  private activatedRoute = inject(ActivatedRoute);
  basketService = inject(BasketService);
  store = inject(StoreService);
  //
  basketItem = computed(() => {
    let basket = this.basketService.basket()?.items;
    return basket?.find(x => x.productId === this.productId) ?? null;
  });
  //properties
  productId!: string;
  product = toSignal(this.store.getProductById(this.activatedRoute.snapshot.params['id']), {
    initialValue: null
  });

  ngOnInit() {
    this.productId = this.activatedRoute.snapshot.params['id'];
  }
  addToBasket() {
    this.basketService.addItemToBasket(this.product()!).subscribe()
  }
  increaseItemQuantity() {
    if (this.basketItem()) {
      this.basketService.increaseItemQuantity(this.basketItem()!).subscribe()
    }
  }
  decreaseItemQuantity() {
    if (this.basketItem()) {
      this.basketService.decreaseItemQuantity(this.basketItem()!).subscribe()
    }
  }
  isInBasket() {
    const item = this.basketService.basket()?.items.find(x => x.productId === this.productId);
    return item !== undefined ? true : false;
  }
  // private getItemFromBasket() {
  //   if (this.basketService.basket() &&
  //     this.basketService.basket()?.items.some(x => x.productId === this.productId)) {
  //     const item = this.basketService.basket()?.items.find(x => x.productId === this.productId!);
  //     if (item) {
  //       this.basketItem.set(item);
  //     }
  //   }
  // }
}
