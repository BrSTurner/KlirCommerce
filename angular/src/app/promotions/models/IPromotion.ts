import { PromotionType } from "./PromotionType";

export interface Promotion{
  id: string,
  name: string,
  description: string,
  type: PromotionType,
  requiredQuantity: number,
  isActive: boolean,
}
