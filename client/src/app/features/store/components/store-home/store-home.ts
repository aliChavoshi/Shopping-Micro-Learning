import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { IPaginate } from '../../../../shared/models/pagination';
import { ICatalog } from '../../../../shared/models/products';
import { ProductItem } from "../product-item/product-item";
import { PaginationModule } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-store-home',
  imports: [ProductItem, PaginationModule],
  templateUrl: './store-home.html',
  styleUrl: './store-home.css'
})
export class StoreHome implements OnInit {
  // private subscriptions = new Subscription();
  paginate!: IPaginate<ICatalog>;
  private storeService = inject(StoreService);

  ngOnInit(): void {
    this.getAllProducts();
  }
  getAllProducts() {
    let sub$ = this.storeService.getAllProducts().subscribe(res => {
      console.log("🚀 ~ StoreHome ~ getAllProducts ~ res:", res)
      this.paginate = res;
    })
    // this.subscriptions.add(sub$);
  }
  // ngOnDestroy(): void {
  //   this.subscriptions.unsubscribe();
  // }
}
