import { AfterContentInit, Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ICatalog } from '../../models/products';
import { StoreService } from '../../services/store.service';
import { AsyncPipe, DecimalPipe } from '@angular/common';
import { Observable, tap } from 'rxjs';
import { BreadcrumbService } from 'xng-breadcrumb'

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css'],
  imports: [RouterLink, DecimalPipe, AsyncPipe]
})
export class ProductDetailComponent implements OnInit {
  private activatedRoute = inject(ActivatedRoute);
  private bcService = inject(BreadcrumbService);
  store = inject(StoreService);
  productId!: string;
  catalog!: Observable<ICatalog>;

  constructor() {
  }

  ngOnInit() {
    this.productId = this.activatedRoute.snapshot.params['id'];
    this.getProductById(this.productId);
  }

  private getProductById(id: string) {
    this.catalog = this.store.getProductById(id)
      .pipe(tap(x => this.bcService.set('@productDetail', x.name)));
  }


}
