export interface ICreateOrder {
  orderLines: string[];
  orderStatus: string;
}

export interface ICreateOrderProps {
  order: ICreateOrder;
}
