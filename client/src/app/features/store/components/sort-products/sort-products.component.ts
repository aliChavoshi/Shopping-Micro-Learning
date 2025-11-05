import { Component, inject, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-sort-products',
  templateUrl: './sort-products.component.html',
  styleUrls: ['./sort-products.component.css'],
  imports: [NgClass]
})
export class SortProductsComponent implements OnInit {
  store = inject(StoreService);
  sortOptions = [
    {
      name: 'Title',
      value: 'title'
    },
    {
      name: 'Price : Descending',
      value: 'priceDesc'
    },
    {
      name: 'Price : Ascending',
      value: 'priceAsc'
    }
  ]
  changeSorting(sort: any) {
    const params = { ...this.store.params(), sort };
    this.store.setParams(params);
  }
  constructor() { }

  ngOnInit() {
  }

}
