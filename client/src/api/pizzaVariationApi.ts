import { PIZZAS_VARIATIONS_URL } from 'src/consts/urls';
import { IPizzaVariation } from 'src/interfaces/pizzaVariation';

export const getPizzasVariations = async (): Promise<IPizzaVariation[]> => {
  const response = await fetch(PIZZAS_VARIATIONS_URL);
  return response.json();
};
