export class CustomResponse<T>{
  data: T;
  success: boolean;
  errors: Array<string>;

  constructor(data: T, success: boolean, errors: Array<string>){
    this.data = data;
    this.success = success;
    this.errors = errors;
  }

}
