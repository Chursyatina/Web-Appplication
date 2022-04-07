import { IPizzaVariation } from './pizzaVariation';

export interface IOrderLine {
  id: number;
  pizzaVariation: IPizzaVariation;
  price: number;
  quantity: number;
}

export interface IOrderLineProps {
  orderLine: IOrderLine;
}
