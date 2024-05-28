import { Component, OnInit } from '@angular/core';
import { CartService } from './cart/services/cart.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Klir E-Commerce';

  constructor(
    private cartService: CartService,
    private router: Router){

  }

  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.cartService.loadShoppingCart();
      }
    });
  }
}
