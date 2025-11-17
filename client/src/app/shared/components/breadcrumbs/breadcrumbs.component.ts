import { Component, inject, OnInit } from '@angular/core';
import { BreadcrumbComponent, BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-breadcrumbs',
  templateUrl: './breadcrumbs.component.html',
  styleUrls: ['./breadcrumbs.component.css'],
  imports: [BreadcrumbComponent]
})
export class BreadcrumbsComponent implements OnInit {
  private bcService = inject(BreadcrumbService);
  showBc = false;
  constructor() {
    this.bcService.breadcrumbs$.subscribe(res => {
      if (res.some(x => x.label === 'Home') && res.length === 1) {
        this.showBc = false;
        return;
      }
      this.showBc = true;
    })
  }

  ngOnInit() {
  }

}
