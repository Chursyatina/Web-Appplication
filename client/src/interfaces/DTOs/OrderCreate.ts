/* eslint-disable */

import { IOrderLineCreate } from "src/interfaces/DTOs/OrderLineCreate";

export interface ICreateOrder {
  OrderLines: IOrderLineCreate[];
}

export interface ICreateOrderProps {
  order: ICreateOrder;
}
