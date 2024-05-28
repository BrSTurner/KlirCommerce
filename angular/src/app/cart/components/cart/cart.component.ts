import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LocalStorageUtils } from 'src/shared/utils/local-storage.utils';
import { Cart } from '../../models/ICart';
import { CustomResponse } from 'src/shared/models/ICustomResponse';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit{
  cart: Cart;

  constructor(private service:  CartService,
    private spinner: NgxSpinnerService) {
      this.cart = LocalStorageUtils.getShoppingCart();
  }

  ngOnInit(): void {
    const cartId = LocalStorageUtils.getCartId();

    if(cartId != null && cartId != ''){
      this.spinner.show();
      this.calculateShoppingCart();
    }
  }

  clearCart(){
    this.spinner.show();
    this.service.clearCart().subscribe({
      next: () => {
        this.cart = LocalStorageUtils.getShoppingCart();
        this.spinner.hide();
      },
      error: () => this.spinner.hide()
    });
  }

  public calculateShoppingCart(){
    const cartId = LocalStorageUtils.getCartId();

    if(cartId == null || cartId == '')
      return;

    this.service.calculate(cartId).subscribe({
      next: (response) => this.handleCalculateSuccess(response),
      error: (errorResponse) => {
        this.service.handleRequestError(errorResponse)
        this.spinner.hide();
      }
    });
  }

  public handleCalculateSuccess(response: CustomResponse<Cart>): void {
    if(response.success){
      this.cart = response.data;
      LocalStorageUtils.saveShoppingCart(this.cart);
    }
    this.spinner.hide();
  }
}
