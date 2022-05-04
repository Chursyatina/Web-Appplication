/* eslint-disable */

import { makeAutoObservable } from 'mobx';
import { insertPizza } from 'src/api/pizzasApi';
import { IPizzaCreate } from 'src/interfaces/DTOs/PizzaCreate';
import { IPizzaUpdate } from 'src/interfaces/DTOs/PizzaUpdate';

import { IIngredient } from 'src/interfaces/ingredient';
import { IPizza } from 'src/interfaces/pizza';

class CreatingPizzaStore {
  basicPizzaPrice = 200;

  id: string = ' ';
  imageLink: string = ' ';
  singleItemImageLink: string = ' ';
  name: string = ' ';
  description: string = ' ';
  ingredients: IIngredient[] = [];
  price = this.basicPizzaPrice;

  constructor() {
    makeAutoObservable(this);
  }

  getEdittingPizza(){
    let ingredientsIds: string[] = [];

    this.ingredients.forEach(ingredient => {
        ingredientsIds.push(ingredient.id);
    })

    let pizza: IPizzaUpdate = {
        Name: this.name,
        Description: this.description,
        ImageLink: this.imageLink,
        SingleItemImageLink: this.singleItemImageLink,
        Ingredients: ingredientsIds
    }

    return pizza;
  }

  getNewPizza(){
    let ingredientsIds: string[] = [];

    this.ingredients.forEach(ingredient => {
        ingredientsIds.push(ingredient.id);
    })

    let pizza: IPizzaCreate = {
        Name: this.name,
        Description: this.description,
        ImageLink: this.imageLink,
        SingleItemImageLink: this.singleItemImageLink,
        Ingredients: ingredientsIds
    }

    return pizza;
  }

  removePizza(){
    this.imageLink = ' ';
    this.singleItemImageLink= ' ';
    this.name = ' ';
    this.description = ' ';
    this.ingredients = [];
    this.price = this.basicPizzaPrice;
  }

  setId(id: string){
    this.id = id;
  }

  setName(newName: string){
    this.name = newName;
  }

  setDescription(newDescription: string){
    this.description = newDescription;
  }

  setImageLink(link:string){
    this.imageLink = link;
  }

  setSingleImageLink(link:string){
    this.singleItemImageLink = link;
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

  changeExistenceOfAdditionalIngredient(ing: IIngredient) {
    if (this.ingredients.find(ingredient => ingredient.id === ing.id) !== undefined) {
      this.ingredients.splice(
        this.ingredients.findIndex(ingredient => ingredient.id === ing.id),
        1,
      );
    } else {
      this.ingredients.push(ing);
    }
    this.recalculatePrice();
  }

  recalculatePrice() {
    this.price = this.basicPizzaPrice;

      this.ingredients.forEach(ingredient => {
        this.price += ingredient.price;
      });
    }
  
}

export const creatingPizzaStore = new CreatingPizzaStore();
