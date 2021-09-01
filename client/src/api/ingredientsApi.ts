import { IIngredient } from 'src/interfaces/ingredient';
import { INGREDIENTS_URL } from 'src/consts/urls';

export const getIngredients = async (): Promise<IIngredient[]> => {
  const response = await fetch(INGREDIENTS_URL);
  return response.json();
};
