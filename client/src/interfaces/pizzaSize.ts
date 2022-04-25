export interface IPizzaSize {
  id: string;
  name: string;
  isDeleted: boolean;
  priceMultiplier: number;
  image: string;
}

export interface IPizzaSizeProps {
  size: IPizzaSize;
}
