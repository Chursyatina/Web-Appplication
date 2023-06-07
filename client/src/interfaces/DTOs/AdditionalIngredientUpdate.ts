/* eslint-disable */

export interface IAdditionalIngredientUpdate {
    Name: string;
    Price: number;
    ImageLink: string;
    IsAvailable: boolean,
  }
  
export interface IAdditionalIngredientUpdateProps {
  id: string;
  additionalIngredient: IAdditionalIngredientUpdate;
}

  