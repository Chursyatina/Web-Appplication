/* eslint-disable */

import { IPizza } from 'src/interfaces/pizza';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { IIngredient } from 'src/interfaces/ingredient';

export interface IPizzaVariationUpdate {
  PizzaId: string;
  SizeId: string;
  DoughId: string;
  Ingredients: string[];
  AdditionalIngredients: string[];
}

export interface IPizzaVariationUpdateProps {
  id: string;
  pizzaVariation: IPizzaVariationUpdate;
}
