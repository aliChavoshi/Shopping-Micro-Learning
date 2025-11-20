import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BasketService } from '../../../features/basket/services/basket.service';

@Component({
  selector: 'app-navbar',
  imports: [RouterModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
  standalone: true
})
export class Navbar {
  basket = inject(BasketService);

  getBasketCount() {
    return this.basket.basket()?.items.reduce((sum, item) => sum + item.quantity, 0);
  }
}
