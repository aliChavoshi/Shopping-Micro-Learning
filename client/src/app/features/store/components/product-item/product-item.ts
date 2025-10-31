import { Component, input } from '@angular/core';
import { ICatalog } from '../../models/products';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-product-item',
  imports: [DecimalPipe],
  templateUrl: './product-item.html',
  styleUrl: './product-item.css'
})
export class ProductItem {
  item = input.required<ICatalog>();
}
