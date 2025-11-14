import { Component, input } from '@angular/core';
import { ICatalog } from '../../models/products';
import { DecimalPipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-item',
  imports: [DecimalPipe, RouterLink],
  templateUrl: './product-item.html',
  styleUrl: './product-item.css'
})
export class ProductItem {
  item = input.required<ICatalog>();
}
