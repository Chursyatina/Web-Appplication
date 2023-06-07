/* eslint-disable */

export interface IPizzaUpdate {
    Name: string;
    Description: string;
    ImageLink: string;
    SingleItemImageLink: string;
    Discount: number;
    BonusCoef: number;
    Ingredients: string[];
}
  
  export interface IPizzaUpdateProps {
    id: string;
    pizza: IPizzaUpdate;
}  