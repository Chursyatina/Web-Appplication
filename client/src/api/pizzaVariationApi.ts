import { PIZZAS_VARIATIONS_URL } from 'src/consts/urls';
import { IPizzaVariationCreate } from 'src/interfaces/DTOs/PizzaVariationCreate';
import { IPizzaVariationUpdateProps } from 'src/interfaces/DTOs/PizzaVariationUpdate';
import { IPizzaVariation } from 'src/interfaces/pizzaVariation';

export const getPizzasVariations = async (): Promise<IPizzaVariation[]> => {
  const response = await fetch(PIZZAS_VARIATIONS_URL);
  return response.json();
};

export const insertPizzaVariation = async (pizzaVariation: IPizzaVariationCreate) => {
  const response = await fetch(PIZZAS_VARIATIONS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(pizzaVariation),
  });
  return (await response.json()) as IPizzaVariation;
};

export const updatePizzaVariation = async (pizzaVariation: IPizzaVariationUpdateProps) => {
  const response = await fetch(`${PIZZAS_VARIATIONS_URL}/${pizzaVariation.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(pizzaVariation.pizzaVariation),
  });
  return (await response.json()) as IPizzaVariation;
};
