export interface IAdditionalIngredient {
  id: string;
  name: string;
  price: number;
  isDeleted: boolean;
  imageLink: string;
  isAvailable: boolean;
}

export interface IAdditionalIngredientProps {
  additionalIngredient: IAdditionalIngredient;
}
