/* eslint-disable */

export interface IIngredientCreate {
  Name: string;
  Price: number;
  ImageLink: string;
}

export interface IIngredientCreateProps {
  order: IIngredientCreate;
}
