import { Component, inject, OnInit, output } from '@angular/core';
import { StoreService } from '../../services/store.service';
import { IType } from '../../models/products';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-filter-types',
  imports: [NgClass],
  templateUrl: './filter-types.html',
  styleUrl: './filter-types.css'
})
export class FilterTypes implements OnInit {
  store = inject(StoreService);
  selectedItem?: IType = { id: '', name: '' };
  ngOnInit(): void {
    this.store.getAllTypes().subscribe();
  }
  selectItem(id: string) {
    this.selectedItem = this.store.types()?.find(x => x.id == id);
    const params = { ...this.store.params(), typeId: id };
    this.store.setParams(params);
  }
}
