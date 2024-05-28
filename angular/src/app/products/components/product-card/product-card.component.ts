import { Component, Input } from '@angular/core';
import { Product } from '../../models/IProduct';
import { CartService } from 'src/app/cart/services/cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {
  @Input()
  product: Product;

  quantity: number = 1;

  constructor(private cartService: CartService){
    this.product = {} as Product
  }

  increaseQuantity() {
    this.quantity++;
  }

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  addToCart() {
    this.cartService.addProductToCart(this.product, this.quantity);
  }

}
