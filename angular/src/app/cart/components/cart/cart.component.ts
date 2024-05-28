import { Component } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LocalStorageUtils } from 'src/shared/utils/local-storage.utils';
import { Cart } from '../../models/ICart';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent {
  cart: Cart;

  constructor(private service:  CartService,
    private spinner: NgxSpinnerService) {
      this.cart = LocalStorageUtils.getShoppingCart();
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
}
