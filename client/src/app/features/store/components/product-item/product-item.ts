import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
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
export class ProductItem {
  basket = inject(BasketService);
  item = input.required<ICatalog>();

  showInUi = computed(() => {
    const basketSnapshot = this.basket.basket();
    return !basketSnapshot?.items.find(x => x.productId == this.item().id);
  });

  addToBasket() {
    this.basket.addItemToBasket(this.item()).subscribe();
  }
}
