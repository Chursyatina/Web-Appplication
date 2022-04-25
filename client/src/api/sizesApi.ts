import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { SIZES_URL } from 'src/consts/urls';
import { ISizeCreate } from 'src/interfaces/DTOs/SizeCreate';
import { ISizeUpdateProps } from 'src/interfaces/DTOs/SizeUpdate';

export const getSizes = async (): Promise<IPizzaSize[]> => {
  const response = await fetch(SIZES_URL);
  return response.json();
};

export const insertSize = async (size: ISizeCreate) => {
  const response = await fetch(SIZES_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(size),
  });
  return (await response.json()) as IPizzaSize;
};

export const updateSize = async (size: ISizeUpdateProps) => {
  const response = await fetch(`${SIZES_URL}/${size.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(size.size),
  });
  return (await response.json()) as IPizzaSize;
};

export const deleteSize = async (id: string) => {
  const response = await fetch(`${SIZES_URL}/${id}`, {
      method: "DELETE",
  });
  return response;
};
