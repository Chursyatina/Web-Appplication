export interface IIngredient {
  id: string;
  name: string;
  price: number;
  imageLink: string;
}

export interface IIngredientProps {
  ingredient: IIngredient;
}
