import { Component, inject, OnInit } from '@angular/core';
import { StoreService } from '../../services/store.service';

@Component({
  selector: 'app-types',
  imports: [],
  templateUrl: './types.html',
  styleUrl: './types.css'
})
export class Types implements OnInit {
  store = inject(StoreService);
  ngOnInit(): void {
    this.store.getAllTypes().subscribe();
  }

}
