/* eslint-disable */

export interface IPizzaCreate {
  Name: string;
  Description: string;
  ImageLink: string;
  SingleItemImageLink: string;
  Discount: number;
  BonusCoef: number;
  Ingredients: string[];
}

export interface IPizzaCreateProps {
  pizza: IPizzaCreate;
}
