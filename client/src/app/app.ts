import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastMessageService } from './core/services/toastMessage.Service';
import { Navbar } from "./pages/navbar/navbar";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  toastMsg = inject(ToastMessageService);
  private http = inject(HttpClient);
  showMsg() {
    this.toastMsg.showMessage('this is test', 'my title', 'success');
  }
  constructor() {
    this.http.get<any>('http://localhost:9010/catalog').subscribe({
      next: (response) => {
        console.log("🚀 ~ App ~ constructor ~ response:", response)
      },
      error: (err) => {
        console.log("🚀 ~ App ~ constructor ~ err:", err)
      }
    })
  }
}
