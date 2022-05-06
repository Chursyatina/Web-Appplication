import { IOrderLine } from 'src/interfaces/orderLine';
import { IOrderStatus } from 'src/interfaces/orderStatus';

export interface IOrder {
  id: string;
  orderLines: IOrderLine[];
  price: number;
  orderStatus: IOrderStatus;
  date: Date;
}

export interface IOrderProps {
  order: IOrder;
}
