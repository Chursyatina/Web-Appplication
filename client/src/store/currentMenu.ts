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
import { deleteAdditionalIngredient, getAdditionalIngredients, insertAdditionalIngredient, updateAdditionalIngredient } from 'src/api/additionalIngredientsApi';
import { IIngredientCreate } from 'src/interfaces/DTOs/IngredientCreate';
import { IIngredientUpdate, IIngredientUpdateProps } from 'src/interfaces/DTOs/IngredientUpdate';
import { IAdditionalIngredientUpdate, IAdditionalIngredientUpdateProps } from 'src/interfaces/DTOs/AdditionalIngredientUpdate';
import { IAdditionalIngredientCreate } from 'src/interfaces/DTOs/AdditionalIngredientCreate';

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
    this.loadData();
  }

  async createAdditionalIngredient(name: string, price: number, image: string) {
    const ingredientCreate: IAdditionalIngredientCreate = {
      Name: name,
      Price: price,
      ImageLink: image,
    };

    const newIngredient: IAdditionalIngredient = await insertAdditionalIngredient(ingredientCreate);
    this.additionalIngredients.push(newIngredient);
    this.loadData();
  }

  async removeIngredient(id: string) {
    await deleteIngredient(id);
    this.ingredients.splice(
      this.ingredients.findIndex(element => element.id === id),
      1,
    );
    this.loadData();
  }

  async removeAdditionalIngredient(id: string) {
    await deleteAdditionalIngredient(id);
    this.additionalIngredients.splice(
      this.additionalIngredients.findIndex(element => element.id === id),
      1,
    );
    this.loadData();
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

    this.ingredients[this.ingredients.findIndex(element => element.id === returnedIngredient.id)] = returnedIngredient;
    this.loadData();
  }

  async updateAdditionalIngredient(id: string, name: string, image: string, price: number) {
    const newIngredient: IAdditionalIngredientUpdate = {
      Name: name,
      ImageLink: image,
      Price: price,
    };

    const ingredientUpdateProps: IAdditionalIngredientUpdateProps = {
      id: id,
      ingredient: newIngredient,
    };

    const returnedIngredient = await updateAdditionalIngredient(ingredientUpdateProps);

    this.additionalIngredients[this.additionalIngredients.findIndex(element => element.id === returnedIngredient.id)] = returnedIngredient;
    this.loadData();
  }
}

export const menuStore = new MenuStore();
