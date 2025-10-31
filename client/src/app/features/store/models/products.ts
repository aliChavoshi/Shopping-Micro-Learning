export interface ICatalog {
  id: string;
  name: string;
  summary: string;
  description: string;
  imageFile: string;
  brands: IBrand;
  types: IType;
  price: number
}
export interface IBrand {
  id: string;
  name: string
}
export interface IType {
  id: string;
  name: string
}
