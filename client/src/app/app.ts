import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastMessageService } from './core/services/toastMessage.Service';
import { Navbar } from "./pages/navbar/navbar";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  toastMsg = inject(ToastMessageService);
  showMsg() {
    this.toastMsg.showMessage('this is test', 'my title', 'success');
  }
}
