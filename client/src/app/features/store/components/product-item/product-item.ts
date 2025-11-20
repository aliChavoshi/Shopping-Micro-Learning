import { Component, inject, input, OnInit, signal } from '@angular/core';
import { ICatalog } from '../../models/products';
import { DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { BasketService } from '../../../basket/services/basket.service';

@Component({
  selector: 'app-product-item',
  imports: [DecimalPipe, RouterLink],
  templateUrl: './product-item.html',
  styleUrl: './product-item.css'
})
export class ProductItem implements OnInit {
  basket = inject(BasketService);
  showInUi = signal(true);
  item = input.required<ICatalog>();

  addToBasket() {
    this.basket.addItemToBasket(this.item()).subscribe(res => {
      if (res) {
        this.showInUi.set(false);
      }
    });
  }
  ngOnInit(): void {
    const basket = this.basket.basket();
    const basketItem = basket?.items.find(x => x.productId == this.item().id);
    //this.showInUi.set(!(result > 0));
    if (basketItem) {
      this.showInUi.set(false);
    } else {
      this.showInUi.set(true);
    }
  }
}
