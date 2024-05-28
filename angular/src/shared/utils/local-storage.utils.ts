import { Cart } from "src/app/cart/models/ICart";

export class LocalStorageUtils{
  static cartKey = 'KlirCart';
  static cartIdKey = 'KlirCartId'

  public static saveShoppingCart(cart: Cart){
    localStorage.setItem(LocalStorageUtils.cartKey, JSON.stringify(cart));
  }

  public static getShoppingCart() : Cart{
    const cart = localStorage.getItem(LocalStorageUtils.cartKey);
    if(cart != null){
      return JSON.parse(cart) as Cart
    }

    return {
      total: 0,
      items: [],
      id: ''
    } as Cart;
  }

  public static saveCartId(cartId: string){
    localStorage.setItem(LocalStorageUtils.cartIdKey, cartId);
  }

  public static getCartId(): string | null{
    const cartId = localStorage.getItem(LocalStorageUtils.cartIdKey);
    return cartId;
  }
}
