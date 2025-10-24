import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'store',
    loadComponent: () => import('./features/store/components/store').then(x => x.Store),
    children: [
      {
        path: '',
        loadComponent: () => import('./features/store/components/store-home/store-home').then(x => x.StoreHome)
      }
    ]
  },
  {
    path: 'not-found',
    loadComponent: () => import('./features/pages/not-found/not-found').then(x => x.NotFound)
  },
  {
    path: 'server-error',
    loadComponent: () => import('./features/pages/server-error/server-error').then(x => x.ServerError)
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./features/pages/unauthorized/unauthorized').then(x => x.Unauthorized)
  },
  {
    path: 'no-internet',
    loadComponent: () => import('./features/pages/no-internet/no-internet').then(x => x.NoInternet)
  },
  {
    path: '**',
    redirectTo: 'not-found'
  }
];
