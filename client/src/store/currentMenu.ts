/* eslint-disable */

import { makeAutoObservable } from 'mobx';

import { IIngredient } from 'src/interfaces/ingredient';
import { IPizza } from 'src/interfaces/pizza';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { deletePizza, getPizzas, insertPizza, updatePizza } from 'src/api/pizzasApi';
import { deleteDough, getDoughs, insertDough, updateDough } from 'src/api/doughsApi';
import { deleteSize, getSizes, insertSize, updateSize } from 'src/api/sizesApi';
import { deleteIngredient, getIngredients, insertIngredient, updateIngredient } from 'src/api/ingredientsApi';
import { deleteAdditionalIngredient, getAdditionalIngredients, insertAdditionalIngredient, updateAdditionalIngredient } from 'src/api/additionalIngredientsApi';
import { IIngredientCreate } from 'src/interfaces/DTOs/IngredientCreate';
import { IIngredientUpdate, IIngredientUpdateProps } from 'src/interfaces/DTOs/IngredientUpdate';
import { IAdditionalIngredientUpdate, IAdditionalIngredientUpdateProps } from 'src/interfaces/DTOs/AdditionalIngredientUpdate';
import { IAdditionalIngredientCreate } from 'src/interfaces/DTOs/AdditionalIngredientCreate';
import { IDoughCreate } from 'src/interfaces/DTOs/DoughCreate';
import { ISizeCreate } from 'src/interfaces/DTOs/SizeCreate';
import { IDoughUpdate, IDoughUpdateProps } from 'src/interfaces/DTOs/DoughUpdate';
import { ISizeUpdate, ISizeUpdateProps } from 'src/interfaces/DTOs/SizeUpdate';
import { IPizzaCreate } from 'src/interfaces/DTOs/PizzaCreate';
import { IPizzaUpdate, IPizzaUpdateProps } from 'src/interfaces/DTOs/PizzaUpdate';

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

  async createPizza(pizza:IPizzaCreate){
    let returnedPizza = await insertPizza(pizza)

    this.pizzas.push(returnedPizza);

    return returnedPizza;
  }

  async updatePizza(id: string, pizza:IPizzaUpdate){
    let updateProps: IPizzaUpdateProps = {
      id: id,
      pizza: pizza,
    }

    let returnedPizza = await updatePizza(updateProps)

    this.pizzas[this.pizzas.findIndex(element => element.id === returnedPizza.id)] = returnedPizza;
    this.loadData();
  }

  async deletePizza(id: string){
    let returned = await deletePizza(id)

    this.pizzas.splice(this.pizzas.findIndex(element => element.id === id),1);
    this.loadData();
  }

  async createIngredient(name: string, price: number, image: string) {
    const ingredientCreate: IIngredientCreate = {
      Name: name,
      Price: price,
      ImageLink: image,
      IsAvailable: true,
      IsObligatory: false,
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
      IsAvailable: true,
    };

    const newIngredient: IAdditionalIngredient = await insertAdditionalIngredient(ingredientCreate);
    this.additionalIngredients.push(newIngredient);
    this.loadData();
  }

  async createDough(name: string, priceMultiplier: number) {
    const doughCreate: IDoughCreate = {
      Name: name,
      PriceMultiplier: priceMultiplier,
    };

    const newDough: IPizzaDough = await insertDough(doughCreate);
    this.doughs.push(newDough);
    this.loadData();
  }

  async createSize(name: string, priceMultiplier: number) {
    const sizeCreate: ISizeCreate = {
      Name: name,
      PriceMultiplier: priceMultiplier,
    };

    const newSize: IPizzaSize = await insertSize(sizeCreate);
    this.sizes.push(newSize);
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

  async removeDough(id: string) {
    await deleteDough(id);
    this.doughs.splice(
      this.doughs.findIndex(element => element.id === id),
      1,
    );
    this.loadData();
  }

  async removeSize(id: string) {
    await deleteSize(id);
    this.sizes.splice(
      this.sizes.findIndex(element => element.id === id),
      1,
    );
    this.loadData();
  }

  async updateIngredient(id: string, name: string, image: string, price: number, isAvailable: boolean, isObligatory: boolean) {
    const newIngredient: IIngredientUpdate = {
      Name: name,
      ImageLink: image,
      Price: price,
      IsAvailable: isAvailable,
      IsObligatory: isObligatory,
    };

    const ingredientUpdateProps: IIngredientUpdateProps = {
      id: id,
      ingredient: newIngredient,
    };

    const returnedIngredient = await updateIngredient(ingredientUpdateProps);

    this.ingredients[this.ingredients.findIndex(element => element.id === returnedIngredient.id)] = returnedIngredient;
    this.loadData();
  }

  async updateAdditionalIngredient(id: string, name: string, image: string, price: number, isAvailable: boolean) {
    const newIngredient: IAdditionalIngredientUpdate = {
      Name: name,
      ImageLink: image,
      Price: price,
      IsAvailable: isAvailable,
    };

    const ingredientUpdateProps: IAdditionalIngredientUpdateProps = {
      id: id,
      additionalIngredient: newIngredient,
    };

    const returnedIngredient = await updateAdditionalIngredient(ingredientUpdateProps);

    this.additionalIngredients[this.additionalIngredients.findIndex(element => element.id === returnedIngredient.id)] = returnedIngredient;
    this.loadData();
  }

  async updateDough(id: string, name: string, priceMultiplier: number) {
    const newDough: IDoughUpdate = {
      Name: name,
      PriceMultiplier: priceMultiplier,
    };

    const doughUpdateProps: IDoughUpdateProps = {
      id: id,
      dough: newDough,
    };

    const returnedDough = await updateDough(doughUpdateProps);

    this.doughs[this.doughs.findIndex(element => element.id === returnedDough.id)] = returnedDough;
    this.loadData();
  }

  async updateSize(id: string, name: string, priceMultiplier: number) {
    const newSize: ISizeUpdate = {
      Name: name,
      PriceMultiplier: priceMultiplier,
    };

    const sizeUpdateProps: ISizeUpdateProps = {
      id: id,
      size: newSize,
    };

    const returnedSize = await updateSize(sizeUpdateProps);

    this.sizes[this.sizes.findIndex(element => element.id === returnedSize.id)] = returnedSize;
    this.loadData();
  }
}



export const menuStore = new MenuStore();
