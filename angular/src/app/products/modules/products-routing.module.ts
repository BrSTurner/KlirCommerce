import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProductsAppComponent } from "../components/products.app.component";
import { CatalogComponent } from "../components/catalog/catalog.component";


const productsRouteConfig: Routes = [
  {
      path: '', component: ProductsAppComponent,
      children: [
          { path: '', component: CatalogComponent }
      ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(productsRouteConfig)
],
exports: [RouterModule]
})
export class ProductsRoutingModule { }
