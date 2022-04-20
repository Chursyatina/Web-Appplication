import { makeAutoObservable } from 'mobx';

import { IOrderLine } from 'src/interfaces/orderLine';
import { ICreateOrder } from 'src/interfaces/DTOs/OrderCreate';
import { getCurrentuser } from 'src/api/usersApi';
import { IBasket } from 'src/interfaces/basket';
import { insertOrder } from 'src/api/ordersApi';

class UserStore {
  isAuthenticated = false;

  basket: IBasket = {
    id: '-1',
    orderLines: [],
    price: 0,
  };

  constructor() {
    makeAutoObservable(this);
  }

  async loadData() {
    const authinfo = getCurrentuser();
    this.isAuthenticated = (await authinfo).isAuth;
    this.basket = (await authinfo).user.basket;
    this.recalculatePrice();
  }

  async createOrder() {
    const order: ICreateOrder = {
      orderLines: [],
      orderStatus: 'some',
    };

    this.basket.orderLines.forEach(element => {
      order.orderLines.push(element.id);
    });

    return await insertOrder(order);
  }

  editOrderLine(id: string, orderLine: IOrderLine) {
    if (this.basket.orderLines.findIndex(element => element.id === id) !== -1) {
      this.basket.orderLines.splice(
        this.basket.orderLines.findIndex(element => element.id === id),
        1,
      );
      this.basket.orderLines.push(orderLine);
    }
  }

  deleteOrderLine(lineId: string) {
    this.basket.orderLines.splice(
      this.basket.orderLines.findIndex(element => element.id === lineId),
      1,
    );
    this.recalculatePrice();
  }

  reduceQuantity(lineId: string) {
    this.basket.orderLines[this.basket.orderLines.findIndex(element => element.id === lineId)].quantity--;
    this.recalculatePrice();
  }

  increaseQuantity(lineId: string) {
    this.basket.orderLines[this.basket.orderLines.findIndex(element => element.id === lineId)].quantity++;
    this.recalculatePrice();
  }

  recalculatePrice() {
    this.basket.price = 0;
    this.basket.orderLines.forEach(element => {
      element.price = element.pizzaVariation.price * element.quantity;
      this.basket.price += element.price;
    });
  }
}

export const userStore = new UserStore();
