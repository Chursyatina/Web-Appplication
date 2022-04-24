import { makeAutoObservable } from 'mobx';

import { IIngredient } from 'src/interfaces/ingredient';
import { IPizza } from 'src/interfaces/pizza';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { getPizzas } from 'src/api/pizzasApi';

class MenuStore {
  pizzas: IPizza[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  async loadData() {
    const result = await getPizzas();
    this.pizzas = result;
  }
}

export const menuStore = new MenuStore();
