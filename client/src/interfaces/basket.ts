import { IOrderLine } from 'src/interfaces/orderLine';
import { IUser } from 'src/interfaces/user';

export interface IBasket {
  id: string;
  orderLines: IOrderLine[];
  price: number;
}

export interface IBasketProps {
  basket: IBasket;
}
