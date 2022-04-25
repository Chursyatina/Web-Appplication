export interface IAdditionalIngredient {
  id: string;
  name: string;
  price: number;
  isDeleted: boolean;
  imageLink: string;
}

export interface IAdditionalIngredientProps {
  additionalIngredient: IAdditionalIngredient;
}
