/* eslint-disable */

import { makeAutoObservable } from 'mobx';

import { IIngredient } from 'src/interfaces/ingredient';
import { IPizza } from 'src/interfaces/pizza';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { getPizzas } from 'src/api/pizzasApi';
import { getDoughs } from 'src/api/doughsApi';
import { getSizes } from 'src/api/sizesApi';
import { deleteIngredient, getIngredients, insertIngredient, updateIngredient } from 'src/api/ingredientsApi';
import { getAdditionalIngredients } from 'src/api/additionalIngredientsApi';
import { IIngredientCreate } from 'src/interfaces/DTOs/IngredientCreate';
import { IIngredientUpdate, IIngredientUpdateProps } from 'src/interfaces/DTOs/IngredientUpdate';

class MenuStore {
  pizzas: IPizza[] = [];
  doughs: IPizzaDough[] = [];
  sizes: IPizzaSize[] = [];
  ingredients: IIngredient[] = [];
  additionalIngredients: IAdditionalIngredient[] = [];

  constructor() {
    makeAutoObservable(this);
  }

  async loadData() {
    this.pizzas = await getPizzas();

    this.doughs = await getDoughs();

    this.sizes = await getSizes();

    this.ingredients = await getIngredients();

    this.additionalIngredients = await getAdditionalIngredients();
  }

  async createIngredient(name: string, price: number, image: string) {
    const ingredientCreate: IIngredientCreate = {
      Name: name,
      Price: price,
      ImageLink: image,
    };

    const newIngredient: IIngredient = await insertIngredient(ingredientCreate);
    this.ingredients.push(newIngredient);
  }

  async removeIngredient(id: string) {
    await deleteIngredient(id);
    this.ingredients.slice(
      this.ingredients.findIndex(element => element.id === id),
      1,
    );
  }

  async updateIngredient(id: string, name: string, image: string, price: number) {
    const newIngredient: IIngredientUpdate = {
      Name: name,
      ImageLink: image,
      Price: price,
    };

    const ingredientUpdateProps: IIngredientUpdateProps = {
      id: id,
      ingredient: newIngredient,
    };

    const returnedIngredient = await updateIngredient(ingredientUpdateProps);

    this.ingredients[this.ingredients.findIndex(element => element.id === returnedIngredient)] = returnedIngredient;
  }
}

export const menuStore = new MenuStore();
