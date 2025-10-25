import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { IPaginate } from '../../../../shared/models/pagination';
import { ICatalog } from '../../../../shared/models/products';
import { ProductItem } from "../product-item/product-item";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { Types } from "../types/types";

@Component({
  selector: 'app-store-home',
  imports: [ProductItem, PaginationModule, Types],
  templateUrl: './store-home.html',
  styleUrl: './store-home.css'
})
export class StoreHome implements OnInit {
  store = inject(StoreService);

  ngOnInit(): void {
    this.getAllProducts();
  }
  getAllProducts() {
    this.store.getAllProducts().subscribe()
  }

}
