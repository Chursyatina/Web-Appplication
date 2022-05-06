import { IPizza } from 'src/interfaces/pizza';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { IOrder } from 'src/interfaces/order';
import { IBasket } from 'src/interfaces/basket';

export interface IUser {
  id: string;
  name: string;
  phone: string;
  basket: IBasket;
  orders: IOrder[];
}

export interface IUserProps {
  user: IUser;
}
