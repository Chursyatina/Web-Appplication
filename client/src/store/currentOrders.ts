/* eslint-disable */

import { makeAutoObservable } from 'mobx';
import { IOrder } from 'src/interfaces/order';
import { getOrders, getOrdersForUser } from 'src/api/ordersApi';
import { userStore } from './currentUser';

class OrdersStore {
  orders: IOrder[] = [];
  filteredOrders: IOrder[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  getDateWithoutTime = (date: Date) => {
    date.setHours(0, 0, 0, 0);
    return date.getTime();
  };

  filterOrders(startDate: Date | null, endDate: Date | null){
    this.filteredOrders = [];

    if (startDate && endDate){
      const start = this.getDateWithoutTime(startDate);
      const end = this.getDateWithoutTime(endDate);

      this.orders.forEach(order => {
        const date = new Date(order.date);
        const currentDate = this.getDateWithoutTime(date);
        if (currentDate >= start && currentDate <= end){
          this.filteredOrders.push(order);
        }
      })
    } else if (startDate && !endDate){
      const start = this.getDateWithoutTime(startDate);

      this.orders.forEach(order => {
        const date = new Date(order.date);
        const currentDate = this.getDateWithoutTime(date);
        if (currentDate >= start){
          this.filteredOrders.push(order);
        }
      })
    } else if (!startDate && endDate){
      const end = this.getDateWithoutTime(endDate);

      this.orders.forEach(order => {
        const date = new Date(order.date);
        const currentDate = this.getDateWithoutTime(date);
        if (currentDate <= end){
          this.filteredOrders.push(order);
        }
      })
    }

    return;
  }

  clearFilter(){
    this.filteredOrders = this.orders;
  }

  async loadData() {
    if (userStore.role === 'admin')
    {
        this.orders = await getOrders();
        this.filteredOrders = this.orders;
    }
    else {
        this.orders = await getOrdersForUser(userStore.id);
        this.filteredOrders = this.orders;
    }
  }
}



export const ordersStore = new OrdersStore();
