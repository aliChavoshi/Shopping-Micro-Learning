import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { Navbar } from "./shared/components/navbar/navbar";
import { FooterComponent } from "./shared/components/footer/footer.component";
import { BreadcrumbsComponent } from "./shared/components/breadcrumbs/breadcrumbs.component";
import { NgxSpinnerModule } from 'ngx-spinner';
import { APP_CONFIG } from './core/config/appConfig.token';
import { BasketService } from './features/basket/services/basket.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule, Navbar, FooterComponent, BreadcrumbsComponent, NgxSpinnerModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private config = inject(APP_CONFIG); //injection in the angular
  private basket = inject(BasketService);
  ngOnInit(): void {
    const loginUser = localStorage.getItem(this.config.basketUsername);
    if(loginUser){
      this.basket.getBasket(loginUser).subscribe(res=>{
        console.log("🚀 ~ App ~ ngOnInit ~ res:", res)
      })
    }
  }

}
