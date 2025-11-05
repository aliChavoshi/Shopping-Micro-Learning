import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { ProductItem } from "../product-item/product-item";
import { PageChangedEvent, PaginationModule } from 'ngx-bootstrap/pagination';
import { FilterTypes } from "../filter-types/filter-types";
import { FilterBrands } from "../filter-brands/filter-brands";
import { IType } from '../../models/products';
import { ProductParams } from '../../models/productParams';

@Component({
  selector: 'app-store-home',
  imports: [ProductItem, PaginationModule, FilterTypes, FilterBrands],
  templateUrl: './store-home.html',
  styleUrl: './store-home.css'
})
export class StoreHome {
  store = inject(StoreService);
  onPageChanged($event: PageChangedEvent) {
    const params = {
      ... this.store.params(), pageIndex: $event.page
    };
    this.store.setParams(params);
  }
  reset() {
    const params = { ... this.store.params(), search: '' };
    this.store.setParams(params);
  }
  filterProducts(value: string) {
    const params = { ... this.store.params(), search: value };
    this.store.setParams(params);
  }
  calculateEndCountPagination() {
    const pageSize = this.store.params().pageSize;
    const pageIndex = this.store.params().pageIndex;
    const count = this.store.products()!.count
    return pageSize * pageIndex > count ? count : (pageSize * pageIndex)
  }
}
