import { AfterContentInit, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ICatalog } from '../../models/products';
import { StoreService } from '../../services/store.service';
import { AsyncPipe, DecimalPipe } from '@angular/common';
import { Observable, tap } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb'
import { BasketService } from '../../../basket/services/basket.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  imports: [RouterLink, DecimalPipe, AsyncPipe]
})
export class ProductDetailComponent implements OnInit {

  private activatedRoute = inject(ActivatedRoute);
  private bcService = inject(BreadcrumbService);
  basketService = inject(BasketService);
  store = inject(StoreService);
  //properties
  productId!: string;
  product!: Observable<ICatalog>;

  constructor() {
  }

  ngOnInit() {
    this.productId = this.activatedRoute.snapshot.params['id'];
    this.getProductById(this.productId);
  }
  addToBasket() {
    //TODO
    this.product.subscribe(product => {
      this.basketService.addItemToBasket(product)
        .subscribe(res => {
          console.log("🚀 ~ ProductDetailComponent ~ addToBasket ~ res:", res)
        })
    })
  }
  public isInBasket() {
    const item = this.basketService.basket()?.items.find(x => x.productId === this.productId);
    return item !== undefined ? true : false;
  }
  private getProductById(id: string) {
    this.product = this.store.getProductById(id)
      .pipe(tap(x => this.bcService.set('@productDetail', x.name)));
  }
}
