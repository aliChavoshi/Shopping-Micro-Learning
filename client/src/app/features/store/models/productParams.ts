export class ProductParams {
  pageIndex = 1;
  pageSize = 9;
  brandId?: string;
  typeId?: string;
  sort: 'priceAsc' | 'priceDesc' = 'priceAsc';
  search?: string;
  count?: number;
}
