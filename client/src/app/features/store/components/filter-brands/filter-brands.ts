import { Component, inject, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-filter-brands',
  imports: [],
  templateUrl: './filter-brands.html',
  styleUrl: './filter-brands.css'
})
export class FilterBrands implements OnInit {
  store = inject(StoreService);
  ngOnInit(): void {
    this.store.getAllBrands().subscribe();
  }
}
