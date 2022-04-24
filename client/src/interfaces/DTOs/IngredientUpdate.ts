/* eslint-disable */

export interface IIngredientUpdate {
    Name: string;
    Price: number;
    ImageLink: string;
  }
  
export interface IIngredientUpdateProps {
  id: string;
  ingredient: IIngredientUpdate;
}

  