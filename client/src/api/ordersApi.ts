import { ORDERS_URL } from 'src/consts/urls';
import { IOrder } from 'src/interfaces/order';
import { ICreateOrder } from 'src/interfaces/DTOs/OrderCreate';

export const getOrders = async (): Promise<IOrder[]> => {
  const response = await fetch(ORDERS_URL);
  return (await response.json()) as IOrder[];
};

export const getOrdersForUser = async (id: string): Promise<IOrder[]> => {
  const response = await fetch(`${ORDERS_URL}/forUser/${id}`);
  return (await response.json()) as IOrder[];
};

export const getOrder = async (id: number): Promise<IOrder> => {
  const response = await fetch(`${ORDERS_URL}/${id}`);
  return response.json();
};

export const insertOrder = async (order: ICreateOrder) => {
  const response = await fetch(ORDERS_URL, {
    method: 'POST',
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(order),
  });
  return response;
};
