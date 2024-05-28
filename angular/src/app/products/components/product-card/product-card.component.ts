import { Component, Input } from '@angular/core';
import { Product } from '../../models/IProduct';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {
  @Input()
  product: Product;

  quantity: number = 1;

  constructor(){
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

  }
}
