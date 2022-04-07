import { IOrderLine } from 'src/interfaces/orderLine';
import { IOrderStatus } from 'src/interfaces/orderStatus';

export interface IOrder {
  id: number;
  orderLines: IOrderLine[];
  price: number;
  orderStatus: IOrderStatus;
}

export interface IOrderProps {
  orderLine: IOrder;
}
