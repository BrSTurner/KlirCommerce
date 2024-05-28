import { Promotion } from "src/app/promotions/models/IPromotion";


export interface Product{
  id: string,
  name: string,
  price: number,
  isActive: boolean,
  isInactive: boolean,
  promotionId: string,
  promotion: Promotion
}
