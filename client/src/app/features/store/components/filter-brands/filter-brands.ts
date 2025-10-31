import { Component, inject, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { IBrand } from '../../../../shared/models/products';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-filter-brands',
  imports: [NgClass],
  templateUrl: './filter-brands.html',
  styleUrl: './filter-brands.css'
})
export class FilterBrands implements OnInit {
  store = inject(StoreService);
  selectedItem?: IBrand = { id: '', name: '' };
  ngOnInit(): void {
    this.store.getAllBrands().subscribe();
  }
  selectItem(brand: IBrand) {
    this.selectedItem = this.store.brands()?.find(x => x.id == brand.id);
  }
}
