import { Component } from '@angular/core';
import { CartService } from 'src/app/cart/services/cart.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: []
})
export class MenuComponent {
  public isCollapsed: boolean;

  constructor(private cartService: CartService) {
    this.isCollapsed = true;
  }

  get totalItensInCart(): number{
    return this.cartService.getTotalItens();
  }
}
