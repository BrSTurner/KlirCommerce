import { Injectable } from '@angular/core';
import { Product } from 'src/app/products/models/IProduct';
import { LocalStorageUtils } from 'src/shared/utils/local-storage.utils';
import { CartItem } from '../models/ICartItem';
import { Cart } from '../models/ICart';
import { HttpClient } from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable, of, tap } from 'rxjs';
import { CustomResponse } from 'src/shared/models/ICustomResponse';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class CartService {

  private apiURL = `${environment.apiURL}/cart`

  constructor(private client: HttpClient,
    private toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  public getTotalItens(){
    var cart = LocalStorageUtils.getShoppingCart();
    return cart.items.map(x => x.quantity).reduce((accumulator, current) => accumulator + current, 0)
  }

  public getShoppingCart(cartId: string) : Observable<CustomResponse<Cart>>{
    if(cartId == null || cartId == ''){
      return of({
        errors: ['Cart identifier is required to obtain your Shopping Cart']
      } as CustomResponse<Cart>)
    }
    return this.client.get<CustomResponse<Cart>>(`${this.apiURL}/${cartId}`);
  }

  public syncShoppingCart(){
    const cartId = LocalStorageUtils.getCartId();
    this.spinner.show();

    if(cartId != null){
      this.updateShoppingCart().subscribe({
        next: (response) => {
          this.toaster.success("Item added to your cart");
          this.handleUpdateCartSuccess(response)
          this.spinner.hide();
        },
        error: (errorResponse) => this.handleRequestError(errorResponse)
      });
      return;
    }

    this.createShoppingCart().subscribe({
      next: (response) => {
        this.handleCreateCartSuccess(response);
        this.spinner.hide();
      },
      error: (errorResponse) => this.handleRequestError(errorResponse)
    });
  }

  public addProductToCart(product: Product, quantity: number){
    var cart = LocalStorageUtils.getShoppingCart();

    if(cart.items.map(x => x.productId).includes(product.id)){
      cart = this.updateProductQuantity(cart, product.id, quantity);
      return;
    }

    const cartId = LocalStorageUtils.getCartId();
    cart.items.push({
      cartId: cartId,
      product: product,
      productId: product.id,
      quantity: quantity,
      unitPrice: product.price,
    } as CartItem)
    LocalStorageUtils.saveShoppingCart(cart);

    this.syncShoppingCart();
  }

  public createShoppingCart(): Observable<CustomResponse<string>>{
    const cart = LocalStorageUtils.getShoppingCart();
    return this.client.post<CustomResponse<string>>(this.apiURL, cart);
  }


  public updateProductQuantity(cart: Cart, productId: string, quantity: number) : Cart{
    cart.items = cart.items.map(item =>{
      if(item.productId == productId)
        item.quantity += quantity;
      return item;
    });

    LocalStorageUtils.saveShoppingCart(cart);

    this.syncShoppingCart();

    return cart;
  }

  public updateShoppingCart(): Observable<CustomResponse<Cart>>{
    const cart = LocalStorageUtils.getShoppingCart();
    const cartId = LocalStorageUtils.getCartId();

    if(cartId == null || cart.items.length <= 0){
      return of({
        errors: ['Unable to update your shopping cart at the moment']
      } as CustomResponse<any>);
    }

    cart.id = cartId as string;
    return this.client.put<CustomResponse<Cart>>(`${this.apiURL}/${cartId}`, cart);
  }

  public loadShoppingCart(){
    const cartId = LocalStorageUtils.getCartId();

    if(cartId == null || cartId == '')
      return;

    this.getShoppingCart(cartId).subscribe({
      next: (response) => this.handleGetCartSuccess(response),
      error: (errorResponse) => this.handleRequestError(errorResponse)
    })
  }

  public clearCart(){
    const cartId = LocalStorageUtils.getCartId();

    return this.client.delete(`${this.apiURL}/${cartId}/items`).pipe(tap(data => {
      const cart = LocalStorageUtils.getShoppingCart();
      cart.total = 0;
      cart.items = [];
      LocalStorageUtils.saveShoppingCart(cart);
    }));
  }

  public calculate(cartId: string){
    return this.client.get<CustomResponse<Cart>>(`${this.apiURL}/${cartId}/calculated`);
  }

  public handleGetCartSuccess(response: CustomResponse<Cart>): void {
    if(response.success){
      LocalStorageUtils.saveShoppingCart(response.data);
    }
  }

  public handleCreateCartSuccess(result: CustomResponse<string>){
    if(result.success){
      LocalStorageUtils.saveCartId(result.data);
      const currentCart = LocalStorageUtils.getShoppingCart();
      currentCart.id = result.data;
      currentCart.items = currentCart.items.map(i => {
        i.cartId = result.data;
        return i;
      });
      LocalStorageUtils.saveShoppingCart(currentCart);
    }
  }

  public handleUpdateCartSuccess(result: CustomResponse<Cart>){
    if(result.success){
      LocalStorageUtils.saveShoppingCart(result.data);
    }
  }

  public handleRequestError(error: CustomResponse<any> | any){

    if(error instanceof CustomResponse) {
      error.errors.forEach((message) => {
        this.toaster.error(message);
      })
    }

    this.spinner.hide();
  }
}
