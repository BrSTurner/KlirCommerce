export interface PagedResult<T>{
  list: Array<T>;
  totalResults: number;
  pageIndex: number;
  pageSize: number;
}
