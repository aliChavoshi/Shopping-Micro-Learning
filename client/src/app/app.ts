import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastMessageService } from './core/services/toastMessage.Service';
import { Navbar } from "./shared/components/navbar/navbar";
import { HttpClient } from '@angular/common/http';
import { APP_CONFIG } from './core/config/appConfig.token';
import { ICatalog } from './shared/models/products';
import { IPaginate } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  constructor() {

  }
}
