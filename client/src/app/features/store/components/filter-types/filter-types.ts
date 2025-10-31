import { Component, inject, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-filter-types',
  imports: [],
  templateUrl: './filter-types.html',
  styleUrl: './filter-types.css'
})
export class FilterTypes implements OnInit {
  store = inject(StoreService);
  ngOnInit(): void {
    this.store.getAllTypes().subscribe();
  }

}
