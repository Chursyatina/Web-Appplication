export interface IIngredient {
  id: string;
  name: string;
  price: number;
  isDeleted: boolean;
  imageLink: string;
  isAvailable: boolean;
  isObligatory: boolean;
}

export interface IIngredientProps {
  ingredient: IIngredient;
}

export interface IIngredientIsPickedProps {
  ingredient: IIngredient;
  isPicked: boolean;
}
