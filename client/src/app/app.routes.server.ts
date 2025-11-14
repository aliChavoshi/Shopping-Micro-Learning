import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: 'not-found',
    renderMode: RenderMode.Prerender
  },
  {
    path: 'contact-us',
    renderMode: RenderMode.Prerender
  },
  {
    path: 'server-error',
    renderMode: RenderMode.Prerender
  },
  {
    path: 'no-internet',
    renderMode: RenderMode.Prerender
  },
  {
    path: 'unauthorized',
    renderMode: RenderMode.Prerender
  },
  {
    path: '**',
    renderMode: RenderMode.Client
  }
];
