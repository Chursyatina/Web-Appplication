/* eslint-disable */

import { makeAutoObservable } from 'mobx';

import { IIngredient } from 'src/interfaces/ingredient';
import { IPizza } from 'src/interfaces/pizza';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { menuStore } from './currentMenu';

class PizzaStore {
  basicPizzaPrice = 200;

  pizza: IPizza = {
    id: '-1',
    imageLink: ' ',
    singleItemImageLink: ' ',
    name: ' ',
    description: ' ',
    price: 0,
    ingredients: [],
  };
  size: IPizzaSize = { id: '-1', name: ' ', priceMultiplier: -1, image: ' ', isDeleted: false };
  dough: IPizzaDough = { id: '-1', name: ' ', priceMultiplier: -1, image: ' ', isDeleted: false };
  ingredients: IIngredient[] = [];
  additionalIngredients: IAdditionalIngredient[] = [];
  price = this.basicPizzaPrice;

  constructor() {
    makeAutoObservable(this);
  }

  removePizza(){
    this.pizza = {
      id: '-1',
    imageLink: ' ',
    singleItemImageLink: ' ',
    name: ' ',
    description: ' ',
    price: 0,
    ingredients: [],
    };
  }

  setPizza(pizza: IPizza) {
    this.pizza = pizza;
    this.setSize(menuStore.sizes[0]);
    this.setDough(menuStore.doughs[0]);
    this.setIngredients(pizza.ingredients);
    this.clearAdditionalIngredients();
  }

  setSize(size: IPizzaSize) {
    this.size = size;
    this.recalculatePrice();
  }

  setDough(dough: IPizzaDough) {
    this.dough = dough;
    this.recalculatePrice();
  }

  setIngredients(ingredients: IIngredient[]) {
    this.ingredients = [...ingredients];
    this.recalculatePrice();
  }

  setAdditionalIngredients(additionalIngredients: IAdditionalIngredient[]) {
    this.additionalIngredients = [...additionalIngredients];
    this.recalculatePrice();
  }

  changeExistenceOfIngredient(newIngredient: IIngredient) {
    if (this.ingredients.find(ingredient => ingredient.id === newIngredient.id) !== undefined) {
      this.ingredients.splice(
        this.ingredients.findIndex(ingredient => ingredient.id === newIngredient.id),
        1,
      );
    } else {
      this.ingredients.push(newIngredient);
    }
    this.recalculatePrice();
  }

  changeExistenceOfAdditionalIngredient(additionalIngredient: IAdditionalIngredient) {
    if (this.additionalIngredients.find(ingredient => ingredient.id === additionalIngredient.id) !== undefined) {
      this.additionalIngredients.splice(
        this.additionalIngredients.findIndex(ingredient => ingredient.id === additionalIngredient.id),
        1,
      );
    } else {
      this.additionalIngredients.push(additionalIngredient);
    }
    this.recalculatePrice();
  }

  clearAdditionalIngredients() {
    this.additionalIngredients = [];
  }

  recalculatePrice() {
    this.price = this.basicPizzaPrice;

    this.price *= this.dough.priceMultiplier;

    if (this.pizza.id !== '-1' && this.size.id !== '-1' && this.dough.id !== '-1') {
      this.ingredients.forEach(ingredient => {
        this.price += ingredient.price;
      });

      this.additionalIngredients.forEach(addAdditionalIngredient => {
        this.price += addAdditionalIngredient.price;
      });

      this.price *= this.size.priceMultiplier;
    }
  }
}

export const pizzaStore = new PizzaStore();
