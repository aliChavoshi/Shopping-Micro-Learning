import { Component, OnInit } from '@angular/core';
import { BreadcrumbComponent } from 'xng-breadcrumb';

@Component({
  selector: 'app-breadcrumbs',
  templateUrl: './breadcrumbs.component.html',
  styleUrls: ['./breadcrumbs.component.css'],
  imports: [BreadcrumbComponent]
})
export class BreadcrumbsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
