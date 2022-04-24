import { IIngredient } from 'src/interfaces/ingredient';
import { ADDITIONALINGREDIENTS_URL } from 'src/consts/urls';

export const getAdditionalIngredients = async (): Promise<IIngredient[]> => {
  const response = await fetch(ADDITIONALINGREDIENTS_URL);
  return response.json();
};
