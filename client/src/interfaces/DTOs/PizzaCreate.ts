/* eslint-disable */

export interface IPizzaCreate {
  Name: string;
  Description: string;
  ImageLink: string;
  SingleItemImageLink: string;
  Ingredients: string[];
}

export interface IPizzaCreateProps {
  pizza: IPizzaCreate;
}
