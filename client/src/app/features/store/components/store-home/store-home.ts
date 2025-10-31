import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { ProductItem } from "../product-item/product-item";
import { PaginationModule } from 'ngx-bootstrap/pagination';
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

}
