/* eslint-disable */

import { makeAutoObservable } from 'mobx';
import { IOrder } from 'src/interfaces/order';
import { getOrders, getOrdersForUser } from 'src/api/ordersApi';
import { userStore } from './currentUser';

class OrdersStore {
  orders: IOrder[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  async loadData() {
    if (userStore.role === 'admin')
    {
        this.orders = await getOrders();
    }
    else {
        this.orders = await getOrdersForUser(userStore.id);
    }
  }
}



export const ordersStore = new OrdersStore();
