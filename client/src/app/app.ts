import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ToastMessage } from './core/services/toast-message';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TooltipModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  toastMsg = inject(ToastMessage);
  showMsg() {
    this.toastMsg.showMessage('this is test', 'my title', 'success');
  }
}
