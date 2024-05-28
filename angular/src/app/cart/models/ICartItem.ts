import { Product } from "src/app/products/models/IProduct";

export interface CartItem{
  cartId: string,
  productId: string,
  product: Product,
  unitPrice: number,
  totalPrice: number,
  quantity: number,
  isPromotionApplied: boolean
}
