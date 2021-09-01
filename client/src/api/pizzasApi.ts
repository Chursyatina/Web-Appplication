import { IPizza } from 'src/interfaces/pizza';
import { PIZZAS_URL } from 'src/consts/urls';

export const getPizzas = async (): Promise<IPizza[]> => {
  const response = await fetch(PIZZAS_URL);
  return response.json();
};
