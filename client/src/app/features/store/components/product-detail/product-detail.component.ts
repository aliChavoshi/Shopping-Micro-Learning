import { AfterContentInit, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ICatalog } from '../../models/products';
import { StoreService } from '../../services/store.service';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  imports: [RouterLink, DecimalPipe]
})
export class ProductDetailComponent implements OnInit {
  private activatedRoute = inject(ActivatedRoute);
  store = inject(StoreService);
  productId!: string;
  product: ICatalog | undefined;

  constructor() {
  }

  ngOnInit() {
    this.productId = this.activatedRoute.snapshot.params['id'];

    if (this.productId) {
      this.getProductById(this.productId);
    }
  }

  private getProductById(id: string) {
    this.store.getProductById(id).subscribe(res => {
      this.product = res;
    });
  }
}
