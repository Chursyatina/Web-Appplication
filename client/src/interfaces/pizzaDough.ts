export interface IPizzaDough {
  id: string;
  name: string;
  isDeleted: boolean;
  priceMultiplier: number;
  image: string;
}

export interface IPizzaDoughProps {
  dough: IPizzaDough;
}
