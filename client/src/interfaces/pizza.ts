import { IIngredient } from './ingredient';

export interface IPizza {
  id: string;
  imageLink: string;
  singleItemImageLink: string;
  name: string;
  description: string;
  price: number;
  ingredients: IIngredient[];
  isAvailable: boolean;
}

export interface IPizzaProps {
  pizza: IPizza;
}
