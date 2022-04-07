import { ORDERS_URL } from 'src/consts/urls';
import { IOrder } from 'src/interfaces/order';

export const getOrders = async (): Promise<IOrder[]> => {
  const response = await fetch(ORDERS_URL);
  return response.json();
};

export const getOrder = async (id: number): Promise<IOrder> => {
  const response = await fetch(`${ORDERS_URL}/${id}`);
  return response.json();
};
