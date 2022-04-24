export interface IIngredient {
  id: string;
  name: string;
  price: number;
  isDeleted: boolean;
  imageLink: string;
}

export interface IIngredientProps {
  ingredient: IIngredient;
}
