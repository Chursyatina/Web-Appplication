import { IIngredient } from 'src/interfaces/ingredient';
import { INGREDIENTS_URL } from 'src/consts/urls';
import { IIngredientCreate } from 'src/interfaces/DTOs/IngredientCreate';
import { IIngredientUpdate, IIngredientUpdateProps } from 'src/interfaces/DTOs/IngredientUpdate';

export const getIngredients = async (): Promise<IIngredient[]> => {
  const response = await fetch(INGREDIENTS_URL);
  return response.json();
};

export const insertIngredient = async (ingredient: IIngredientCreate) => {
  const response = await fetch(INGREDIENTS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(ingredient),
  });
  return (await response.json()) as IIngredient;
};

export const updateIngredient = async (ingredient: IIngredientUpdateProps) => {
  const response = await fetch(`${INGREDIENTS_URL}/${ingredient.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(ingredient.ingredient),
  });
  return (await response.json()) as IIngredient;
};

export const deleteIngredient = async (id: string) => {
  const response = await fetch(`${INGREDIENTS_URL}/${id}`, {
      method: "DELETE",
  });
  return response;
};
