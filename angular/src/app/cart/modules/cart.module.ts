import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from '../components/cart/cart.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { CartService } from '../services/cart.service';
import { CartRoutingModule } from './cart-routing.module';
import { CartAppComponent } from '../components/cart.app.component';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [
    CartAppComponent,
    CartComponent
  ],
  imports: [
    CommonModule,
    NgxSpinnerModule,
    CartRoutingModule,
    HttpClientModule
  ],
  providers: [
    CartService
  ]
})
export class CartModule { }
