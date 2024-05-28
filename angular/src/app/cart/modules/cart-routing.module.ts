import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartComponent } from '../components/cart/cart.component';
import { CartAppComponent } from '../components/cart.app.component';



const cartRouteConfig: Routes = [
  {
      path: '', component: CartAppComponent,
      children: [
          { path: '', component: CartComponent }
      ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(cartRouteConfig)
],
exports: [RouterModule]
})
export class CartRoutingModule { }
