export interface IBasket {
  userName: string;
  totalPrice: number;
  items: IBasketItem[];
}
export interface IBasketItem {
  quantity: number;
  price: number;
  productId: number;
  imageFile: string;
  productName: string
}
export class Basket implements IBasket {
  constructor(userName: string) {
    this.userName = userName;
    this.items = [];
    this.totalPrice = 0;
  }
  userName: string;
  totalPrice: number;
  items: IBasketItem[];
}
