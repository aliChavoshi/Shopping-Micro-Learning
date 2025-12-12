import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { StoreService } from '../../services/store.service';
import { DecimalPipe } from '@angular/common';
import { BasketService } from '../../../basket/services/basket.service';
import { toSignal } from '@angular/core/rxjs-interop';
import { IBasketItem } from '../../../basket/models/basket';

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
  basketItem = signal<IBasketItem | null>(null);
  //properties
  productId!: string;
  product = toSignal(this.store.getProductById(this.activatedRoute.snapshot.params['id']), {
    initialValue: null
  });

  constructor() {
  }

  ngOnInit() {
    this.productId = this.activatedRoute.snapshot.params['id'];
    this.getItemFromBasket();
  }
  addToBasket() {
    this.basketService.addItemToBasket(this.product()!)
      .subscribe(res => {
        if (res) {
          let itemBasket = res.items.find(x => x.productId == this.productId);
          if (itemBasket) {
            this.basketItem.set(itemBasket);
          }
        }
      })
  }
  public isInBasket() {
    const item = this.basketService.basket()?.items.find(x => x.productId === this.productId);
    return item !== undefined ? true : false;
  }
  private getItemFromBasket() {
    if (this.basketService.basket() &&
      this.basketService.basket()?.items.some(x => x.productId === this.productId)) {
      const item = this.basketService.basket()?.items.find(x => x.productId === this.productId!);
      if (item) {
        this.basketItem.set(item);
      }
    }
  }
  // private getProductById(id: string) {
  //   this.product = this.store.getProductById(id)
  //     .pipe(tap(x => this.bcService.set('@productDetail', x.name)));
  // }
}
