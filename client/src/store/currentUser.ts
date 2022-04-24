/* eslint-disable */

import { makeAutoObservable } from 'mobx';

import { IOrderLine } from 'src/interfaces/orderLine';
import { ICreateOrder } from 'src/interfaces/DTOs/OrderCreate';
import { getCurrentuser } from 'src/api/usersApi';
import { IBasket } from 'src/interfaces/basket';
import { insertOrder } from 'src/api/ordersApi';
import { IPizzaVariation } from 'src/interfaces/pizzaVariation';
import { IPizza } from 'src/interfaces/pizza';
import { IIngredient } from 'src/interfaces/ingredient';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { IPizzaVariationCreate } from 'src/interfaces/DTOs/PizzaVariationCreate';
import { insertPizzaVariation } from 'src/api/pizzaVariationApi';

import { pizzaStore } from './currentPizza';
import { IOrderLineCreate } from 'src/interfaces/DTOs/OrderLineCreate';

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
      OrderLines: [],
    };

    this.basket.orderLines.forEach(element => {
      var createLine : IOrderLineCreate = {
        PizzaVariationId : element.pizzaVariation.id,
        Quantity : element.quantity,
      }

      order.OrderLines.push(createLine);
    });

    return await insertOrder(order);
  }

  async addCurrentPizzaToBasket() {
    const newPizzaVariation: IPizzaVariation = {
      id: 'newId',
      pizza: pizzaStore.pizza,
      ingredients: pizzaStore.ingredients,
      additionalIngredients: pizzaStore.additionalIngredients,
      price: pizzaStore.price,
      size: pizzaStore.size,
      dough: pizzaStore.dough,
    };

    const orderLine: IOrderLine = {
      id: 'someId',
      pizzaVariation: newPizzaVariation,
      price: newPizzaVariation.price,
      quantity: 1,
    };

    var pizza : IPizzaVariation = await this.addPizzaVariationToDataBase(newPizzaVariation);

    orderLine.pizzaVariation.id = pizza.id;
    console.log('ниже находится айдишник новой пиццы');
    console.log(orderLine.pizzaVariation.id);
    this.basket.orderLines.push(orderLine);
    console.log(this.basket.orderLines.length);
    this.recalculatePrice();
  }

  addPizzaVariationToDataBase(pizzaVariation: IPizzaVariation) {
    const pizzaVariationCreate: IPizzaVariationCreate = {
      PizzaId: pizzaVariation.pizza.id,
      SizeId: pizzaVariation.size.id,
      DoughId: pizzaVariation.dough.id,
      Ingredients: [],
      AdditionalIngredients: [],
    };

    pizzaVariation.ingredients.forEach(element => {
      pizzaVariationCreate.Ingredients.push(element.id);
    });

    pizzaVariation.additionalIngredients.forEach(element => {
      pizzaVariationCreate.AdditionalIngredients.push(element.id);
    });

    return insertPizzaVariation(pizzaVariationCreate);
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

  clearBasket(){
    this.basket.orderLines = [];
  }

  deleteOrderLine(line: IOrderLine) {
    this.basket.orderLines.splice(
      this.basket.orderLines.findIndex(element => element === line),
      1,
    );
    this.recalculatePrice();
  }

  reduceQuantity(line: IOrderLine) {
    this.basket.orderLines[this.basket.orderLines.findIndex(element => element === line)].quantity--;
    this.recalculatePrice();
  }

  increaseQuantity(line: IOrderLine) {
    this.basket.orderLines[this.basket.orderLines.findIndex(element => element === line)].quantity++;
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
