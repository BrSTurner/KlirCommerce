import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  {
    path: 'products',
    loadChildren: () => import('./products/modules/products.module')
      .then(m => m.ProductsModule)
  },
  {
    path: 'cart',
    loadChildren: () => import('./cart/modules/cart.module')
      .then(m => m.CartModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
