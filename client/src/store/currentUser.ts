/* eslint-disable */

import { makeAutoObservable } from 'mobx';

import { IOrderLine } from 'src/interfaces/orderLine';
import { ICreateOrder } from 'src/interfaces/DTOs/OrderCreate';
import { getCurrentuser, signIn, signOut, signUp } from 'src/api/usersApi';
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
import { ISignInForm } from 'src/interfaces/DTOs/SignInForm';
import { ISignUpForm } from 'src/interfaces/DTOs/SignUpForm';

class UserStore {
  isAuthenticated = false;
  role = "";

  basket: IBasket = {
    id: '-1',
    orderLines: [],
    price: 0,
  };

  constructor() {
    makeAutoObservable(this);
  }

  async signIn(phone: string, password: string){
    let normalizedPhone = '8';
    normalizedPhone += phone.substring(4,7);
    normalizedPhone += phone.substring(9,12);
    normalizedPhone += phone.substring(13,15);
    normalizedPhone += phone.substring(16);

    let form: ISignInForm = {
      Phone: normalizedPhone,
      Password: password,
      RememberMe: true,
      ReturnUrl:'',
    };

    await signIn(form);
    await this.loadData();
  }

  async signUp(phone: string, password: string, passwordConfirm: string){
    let normalizedPhone = '8';
    normalizedPhone += phone.substring(4,7);
    normalizedPhone += phone.substring(9,12);
    normalizedPhone += phone.substring(13,15);
    normalizedPhone += phone.substring(16);

    let form: ISignUpForm = {
      Phone: normalizedPhone,
      Password: password,
      PasswordConfirm: passwordConfirm
    };

    await signUp(form);
    await this.loadData();
  }

  async signOut(){
    await signOut();
    this.isAuthenticated = false;
    this.role = '';
    await this.loadData();
  }

  async loadData() {
    const authinfo = await getCurrentuser();
    this.isAuthenticated = (await authinfo).isAuth;
    if (this.isAuthenticated)
    {
    this.role = (await authinfo).role;
    if (this.basket.orderLines.length !== 0)
    {
      this.basket.id == (await authinfo).user.basket.id;
    }
    else{
      this.basket = (await authinfo).user.basket;
    }
    this.recalculatePrice();
    }

    console.log(this.basket.orderLines.length);
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
    this.recalculatePrice();
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
