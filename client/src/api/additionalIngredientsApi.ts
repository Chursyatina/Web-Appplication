import { IIngredient } from 'src/interfaces/ingredient';
import { ADDITIONALINGREDIENTS_URL } from 'src/consts/urls';
import { IAdditionalIngredient } from 'src/interfaces/additionalIngredient';
import { IAdditionalIngredientCreate } from 'src/interfaces/DTOs/AdditionalIngredientCreate';
import { IAdditionalIngredientUpdateProps } from 'src/interfaces/DTOs/AdditionalIngredientUpdate';

export const getAdditionalIngredients = async (): Promise<IAdditionalIngredient[]> => {
  const response = await fetch(ADDITIONALINGREDIENTS_URL);
  return response.json();
};

export const insertAdditionalIngredient = async (ingredient: IAdditionalIngredientCreate) => {
  const response = await fetch(ADDITIONALINGREDIENTS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(ingredient),
  });
  return (await response.json()) as IIngredient;
};

export const updateAdditionalIngredient = async (ingredient: IAdditionalIngredientUpdateProps) => {
  const response = await fetch(`${ADDITIONALINGREDIENTS_URL}/${ingredient.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(ingredient.additionalIngredient),
  });
  return (await response.json()) as IIngredient;
};

export const deleteAdditionalIngredient = async (id: string) => {
  const response = await fetch(`${ADDITIONALINGREDIENTS_URL}/${id}`, {
      method: "DELETE",
  });
  return response;
};
