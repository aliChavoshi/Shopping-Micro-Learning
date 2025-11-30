import { Component, inject, OnInit } from '@angular/core';
import { BasketService } from '../../services/basket.service';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-basket-total',
  templateUrl: './basket-total.component.html',
  styleUrls: ['./basket-total.component.css'],
  imports: [DecimalPipe]
})
export class BasketTotalComponent implements OnInit {
  basket = inject(BasketService);
  constructor() { }

  ngOnInit() {
  }

}
