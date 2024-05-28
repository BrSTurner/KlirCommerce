import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCardComponent } from '../components/product-card/product-card.component';
import { CatalogComponent } from '../components/catalog/catalog.component';
import { ProductsAppComponent } from '../components/products.app.component';
import { ProductsRoutingModule } from './products-routing.module';
import { ProductsService } from '../services/products.service';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HttpClientModule } from '@angular/common/http'



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
    ProductsService
  ]
})
export class ProductsModule { }
