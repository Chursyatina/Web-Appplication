/* eslint-disable */

export interface IAdditionalIngredientCreate {
    Name: string;
    Price: number;
    ImageLink: string;
    IsAvailable: boolean,
  }
  
  export interface IAdditionalIngredientCreateProps {
    additionalIngredient: IAdditionalIngredientCreate;
  }
  