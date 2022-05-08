/* eslint-disable */

export interface IIngredientUpdate {
    Name: string;
    Price: number;
    ImageLink: string;
    IsAvailable: boolean,
  }
  
export interface IIngredientUpdateProps {
  id: string;
  ingredient: IIngredientUpdate;
}

  