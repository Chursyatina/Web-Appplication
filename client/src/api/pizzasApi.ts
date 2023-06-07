import { IPizza } from 'src/interfaces/pizza';
import { PIZZAS_URL } from 'src/consts/urls';
import { IPizzaCreate } from 'src/interfaces/DTOs/PizzaCreate';
import { IPizzaUpdateProps } from 'src/interfaces/DTOs/PizzaUpdate';

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

export const updatePizza = async (pizza: IPizzaUpdateProps) => {
  const response = await fetch(`${PIZZAS_URL}/${pizza.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(pizza.pizza),
  });
  return (await response.json()) as IPizza;
};

export const deletePizza = async (id: string) => {
  const response = await fetch(`${PIZZAS_URL}/${id}`, {
      method: "DELETE",
  });
  return response;
};
