/* eslint-disable */

export interface IPizzaUpdate {
    Name: string;
    Description: string;
    ImageLink: string;
    SingleImageLink: string;
    Ingredients: string[];
}
  
  export interface IPizzaUpdateProps {
    pizza: IPizzaUpdate;
}  