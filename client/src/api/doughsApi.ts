import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { DOUGHS_URL } from 'src/consts/urls';

export const getDoughs = async (): Promise<IPizzaDough[]> => {
  const response = await fetch(DOUGHS_URL);
  return response.json();
};
