export interface IIngredient {
  id: number;
  name: string;
  price: number;
  imageLink: string;
}

export interface IIngredientProps {
  ingredient: IIngredient;
}
