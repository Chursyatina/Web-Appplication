import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { SIZES_URL } from 'src/consts/urls';

export const getSizes = async (): Promise<IPizzaSize[]> => {
  const response = await fetch(SIZES_URL);
  return response.json();
};
