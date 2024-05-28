import { CartItem } from "./ICartItem";

export interface Cart{
  id: string,
  total: number,
  items: Array<CartItem>
}
