import { IPizza } from 'src/interfaces/pizza';
import { PIZZAS_URL } from 'src/consts/urls';
import { IPizzaCreate } from 'src/interfaces/DTOs/PizzaCreate';

export const getPizzas = async (): Promise<IPizza[]> => {
  const response = await fetch(PIZZAS_URL);
  return response.json();
};

export const insertPizza = async (pizza: IPizzaCreate) => {
  const response = await fetch(PIZZAS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(pizza),
  });
  return (await response.json()) as IPizza;
};
