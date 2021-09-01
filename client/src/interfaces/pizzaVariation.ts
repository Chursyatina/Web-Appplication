import { IPizza } from 'src/interfaces/pizza';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IPizzaDough } from 'src/interfaces/pizzaDough';

import { IAdditionalIngredient } from './additionalIngredient';
import { IIngredient } from './ingredient';

export interface IPizzaVariation {
  id: number;
  pizza: IPizza;
  size: IPizzaSize;
  dough: IPizzaDough;
  price: number;
  ingredients: IIngredient[];
  additionalIngredients: IAdditionalIngredient[];
}

export interface IPizzaVariationProps {
  pizzaVariation: IPizzaVariation;
}
