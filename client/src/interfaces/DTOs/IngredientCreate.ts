/* eslint-disable */

export interface IIngredientCreate {
  Name: string;
  Price: number;
  ImageLink: string;
  IsAvailable: boolean,
  IsObligatory: boolean,
}

export interface IIngredientCreateProps {
  order: IIngredientCreate;
}
