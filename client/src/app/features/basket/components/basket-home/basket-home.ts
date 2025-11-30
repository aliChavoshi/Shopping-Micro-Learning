import { Component, inject } from '@angular/core';
import { BasketService } from '../../services/basket.service';
import { DecimalPipe } from '@angular/common';
import { BasketTotalComponent } from "../basket-total/basket-total.component";

@Component({
  selector: 'app-basket-home',
  imports: [DecimalPipe, BasketTotalComponent],
  templateUrl: './basket-home.html',
  styleUrl: './basket-home.css'
})
export class BasketHome {
  basket = inject(BasketService);
}
