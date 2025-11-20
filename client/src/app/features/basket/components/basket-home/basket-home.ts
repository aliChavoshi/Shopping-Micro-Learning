import { Component, inject } from '@angular/core';
import { BasketService } from '../../services/basket.service';
import { DecimalPipe } from '@angular/common';

@Component({
  selector: 'app-basket-home',
  imports: [DecimalPipe],
  templateUrl: './basket-home.html',
  styleUrl: './basket-home.css'
})
export class BasketHome {
  basket = inject(BasketService);
}
