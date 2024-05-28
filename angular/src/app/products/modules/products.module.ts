import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCardComponent } from '../components/product-card/product-card.component';
import { CatalogComponent } from '../components/catalog/catalog.component';
import { ProductsAppComponent } from '../components/products.app.component';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductsService } from '../services/products.service';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HttpClientModule } from '@angular/common/http'
import { CartService } from 'src/app/cart/services/cart.service';



@NgModule({
  declarations: [
    ProductsAppComponent,
    ProductCardComponent,
    CatalogComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    NgxSpinnerModule,
    HttpClientModule
  ],
  providers: [
    ProductsService,
    CartService
  ]
})
export class ProductsModule { }
