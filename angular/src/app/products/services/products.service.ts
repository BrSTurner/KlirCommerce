import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CustomResponse } from 'src/shared/models/ICustomResponse';
import { PagedResult } from 'src/shared/models/IPagedResult';
import { Product } from '../models/IProduct';

@Injectable()
export class ProductsService {

  apiURL = `${environment.apiURL}/products`;

  constructor(private client: HttpClient) {}

  public getAllProducts() : Observable<CustomResponse<PagedResult<Product>>>{
    return this.client.get<CustomResponse<PagedResult<Product>>>(`${this.apiURL}/paged`);
  }
}
