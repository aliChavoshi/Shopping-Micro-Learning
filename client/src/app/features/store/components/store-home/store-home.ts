import { Component, inject, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { IPaginate } from '../../../../shared/models/pagination';
import { ICatalog } from '../../../../shared/models/products';

@Component({
  selector: 'app-store-home',
  imports: [],
  templateUrl: './store-home.html',
  styleUrl: './store-home.css'
})
export class StoreHome implements OnInit {
  paginate!: IPaginate<ICatalog>;
  private storeService = inject(StoreService);

  ngOnInit(): void {
    this.getAllProducts();
  }
  getAllProducts() {
    this.storeService.getAllProducts().subscribe(res => {
      this.paginate = res;
    })
  }
}
