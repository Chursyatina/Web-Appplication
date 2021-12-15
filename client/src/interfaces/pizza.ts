import { IIngredient } from './ingredient';

export interface IPizza {
  id: number;
  imageLink: string;
  singleItemImageLink: string;
  name: string;
  description: string;
  price: number;
  ingredients: IIngredient[];
}

export interface IPizzaProps {
  pizza: IPizza;
}