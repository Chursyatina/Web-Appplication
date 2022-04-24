import { PIZZAS_VARIATIONS_URL } from 'src/consts/urls';
import { IPizzaVariationCreate } from 'src/interfaces/DTOs/PizzaVariationCreate';
import { IPizzaVariation } from 'src/interfaces/pizzaVariation';

export const getPizzasVariations = async (): Promise<IPizzaVariation[]> => {
  const response = await fetch(PIZZAS_VARIATIONS_URL);
  return response.json();
};

export const insertPizzaVariation = async (order: IPizzaVariationCreate) => {
  const response = await fetch(PIZZAS_VARIATIONS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(order),
  });
  return (await response.json()) as IPizzaVariation;
};
