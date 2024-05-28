export interface CustomResponse<T>{
  data: T,
  success: boolean,
  errors: Array<string>
}
