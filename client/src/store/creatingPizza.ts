/* eslint-disable */

import { makeAutoObservable } from 'mobx';

import { IIngredient } from 'src/interfaces/ingredient';

class CreatingPizzaStore {
  basicPizzaPrice = 200;

  imageLink: string = ' ';
  singleItemImageLink: string = ' ';
  name: string = ' ';
  description: string = ' ';
  ingredients: IIngredient[] = [];
  price = this.basicPizzaPrice;

  constructor() {
    makeAutoObservable(this);
  }

  createNewPizza(){
    console.log(this.name);
    console.log(this.description);
    console.log(this.ingredients);
    console.log(this.imageLink);
    console.log(this.singleItemImageLink);
  }

  removePizza(){
    this.imageLink = ' ';
    this.singleItemImageLink= ' ';
    this.name = ' ';
    this.description = ' ';
    this.ingredients = [];
    this.price = this.basicPizzaPrice;
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
    console.log('addedone');
    this.recalculatePrice();
    console.log(this.price);
    console.log(this.name);
    console.log(this.description);
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
