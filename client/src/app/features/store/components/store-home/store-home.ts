import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { IPaginate } from '../../../../shared/models/pagination';
import { ICatalog } from '../../../../shared/models/products';
import { Subscription } from 'rxjs';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-store-home',
  imports: [DecimalPipe],
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
