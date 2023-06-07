import { IPizzaVariation } from './pizzaVariation';

export interface IOrderLine {
  id: string;
  pizzaVariation: IPizzaVariation;
  price: number;
  quantity: number;
}

export interface IOrderLineProps {
  orderLine: IOrderLine;
}
