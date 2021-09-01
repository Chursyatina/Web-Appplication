import React, { useEffect, useState } from 'react';
import { Grid } from '@material-ui/core';

import { IPizzaVariation } from 'src/interfaces/pizzaVariation';
import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';
import { getPizzasVariations } from 'src/api/pizzaVariationApi';

import { PizzaInBasket } from './PizzaInBasket';

export const PizzaInBasketList = () => {
  const { root } = pizzaListStyles();

  const [pizzasVariations, setPizzasVariaitons] = useState<IPizzaVariation[]>([]);
  useEffect(() => {
    const getPizzasVariationsList = async () => {
      const result = await getPizzasVariations();
      setPizzasVariaitons(result);
    };
    getPizzasVariationsList();
  }, []);

  return (
    <Grid container justify="center" className={root}>
      <Grid item>
        {pizzasVariations.map(pizzaVariation => (
          <PizzaInBasket key={pizzaVariation.id} pizzaVariation={pizzaVariation} />
        ))}
      </Grid>
    </Grid>
  );
};
