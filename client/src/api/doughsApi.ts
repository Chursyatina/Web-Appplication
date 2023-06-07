import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { DOUGHS_URL } from 'src/consts/urls';
import { IDoughCreate } from 'src/interfaces/DTOs/DoughCreate';
import { IDoughUpdateProps } from 'src/interfaces/DTOs/DoughUpdate';

export const getDoughs = async (): Promise<IPizzaDough[]> => {
  const response = await fetch(DOUGHS_URL);
  return response.json();
};

export const insertDough = async (dough: IDoughCreate) => {
  const response = await fetch(DOUGHS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(dough),
  });
  return (await response.json()) as IPizzaDough;
};

export const updateDough = async (dough: IDoughUpdateProps) => {
  const response = await fetch(`${DOUGHS_URL}/${dough.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(dough.dough),
  });
  return (await response.json()) as IPizzaDough;
};

export const deleteDough = async (id: string) => {
  const response = await fetch(`${DOUGHS_URL}/${id}`, {
      method: "DELETE",
  });
  return response;
};
