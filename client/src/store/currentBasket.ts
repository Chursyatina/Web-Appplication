import { makeAutoObservable } from 'mobx';

import { IIngredient } from 'src/interfaces/ingredient';
import { IPizza } from 'src/interfaces/pizza';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { getOrder } from 'src/api/ordersApi';
import { IOrderLine } from 'src/interfaces/orderLine';
import { IOrder } from 'src/interfaces/order';
import { IOrderStatus } from 'src/interfaces/orderStatus';

class BasketStore {
  orderLines: IOrderLine[] = [];

  order: IOrder = {
    id: 1,
    orderLines: [],
    price: 0,
    orderStatus: {
      id: -1,
      name: '',
    },
  };

  price = 0;

  constructor() {
    makeAutoObservable(this);
  }

  async loadData(id: number) {
    this.order = await getOrder(id);
  }

  setBasket(basket: IOrder) {
    this.order = basket;
    this.orderLines = basket.orderLines;
    this.price = basket.price;
  }

  getOrderLines() {
    return this.orderLines;
  }

  editOrderLine(id: number, orderLine: IOrderLine) {
    if (this.orderLines.findIndex(element => element.id === id) !== -1) {
      this.orderLines.splice(
        this.orderLines.findIndex(element => element.id === id),
        1,
      );
      this.orderLines.push(orderLine);
    }
  }

  recalculatePrice() {
    this.price = 0;
    this.orderLines.forEach(element => {
      this.price += element.price * element.quantity;
    });
  }
}

export const basketStore = new BasketStore();
