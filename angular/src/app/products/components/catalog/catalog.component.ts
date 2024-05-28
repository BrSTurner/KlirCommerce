import { Component } from '@angular/core';
import { PagedResult } from 'src/shared/models/IPagedResult';
import { Product } from '../../models/IProduct';
import { CustomResponse } from 'src/shared/models/ICustomResponse';
import { ProductsService } from '../../services/products.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
  pagedResult: PagedResult<Product>;

  constructor(private service: ProductsService,
    private spinner: NgxSpinnerService,
    private toaster: ToastrService
  ){
    this.pagedResult = {
      totalResults: 0,
      pageIndex: 1,
      pageSize: 6,
      list: []
    };
  }

  ngOnInit(): void {
    this.loadAllProducts();
  }

  private loadAllProducts(){
    this.spinner.show();
    this.service
        .getAllProducts()
        .subscribe({
          next: (response) => this.handleResponse(response),
          error: (errorResult) => this.handleError(errorResult)
        });
  }

  private handleResponse(result: CustomResponse<PagedResult<Product>>){
    if(result.success){
      this.pagedResult = result.data;
    }
    this.spinner.hide();
  }

  private handleError(error: CustomResponse<any> | any){
    if(error instanceof CustomResponse) {
      error.errors.forEach((message) => {
        this.toaster.error(message);
      })
    }
    this.spinner.hide();
  }
}
