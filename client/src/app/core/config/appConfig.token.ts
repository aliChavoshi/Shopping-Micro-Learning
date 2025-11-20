import { InjectionToken } from "@angular/core";
import { IAppConfig } from './appConfig.models';

export const APP_CONFIG = new InjectionToken<IAppConfig>('APP_CONFIG', {
  providedIn: 'root',
  factory: () => ({
    baseUrl: 'http://localhost:9010',
    basketUsername: 'basket_userName'
  })
})
