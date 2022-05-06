/* eslint-disable */

import { makeAutoObservable } from 'mobx';
import { IOrder } from 'src/interfaces/order';
import { getOrders } from 'src/api/ordersApi';

class OrdersStore {
  orders: IOrder[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  async loadData() {
    this.orders = await getOrders();
  }
}



export const ordersStore = new OrdersStore();
