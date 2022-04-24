/* eslint-disable */

export interface IOrderLineCreate {
    PizzaVariationId: string;
    Quantity: number;
  }
  
  export interface IOrderLineCreateprops {
    order: IOrderLineCreate;
  }
  