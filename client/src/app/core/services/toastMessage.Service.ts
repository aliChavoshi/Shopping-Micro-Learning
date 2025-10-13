import { inject, Injectable } from '@angular/core';
import { IndividualConfig, ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastMessageService {
  private toast = inject(ToastrService);
  private baseConfig: Partial<IndividualConfig> = {
    timeOut: 5000,
    progressBar: true,
    closeButton: true,
    tapToDismiss: true,
    toastClass: 'ngx-toastr custom-toast shadow',
  };
  showMessage(message: string, title: string, type: 'success' | 'warning' | 'info' | 'error') {
    const iconMap: Record<typeof type, string> = {
      success: '✅',
      warning: '⚠️',
      info: 'ℹ️',
      error: '❌',
    };
    const finalMessage = `${iconMap[type]} ${message}`;

    switch (type) {
      case 'success':
        this.toast.success(finalMessage, title, this.baseConfig);
        break;
      case 'warning':
        this.toast.warning(finalMessage, title, this.baseConfig);
        break;
      case 'info':
        this.toast.info(finalMessage, title, this.baseConfig);
        break;
      case 'error':
        this.toast.error(finalMessage, title, this.baseConfig);
        break;
      default:
        this.toast.show(finalMessage, title, this.baseConfig); //black
        break;
    }
  }
}
