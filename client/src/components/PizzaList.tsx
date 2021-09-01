import React, { useState, useEffect } from 'react';
import { Grid } from '@material-ui/core';

import { IPizza } from 'src/interfaces/pizza';
import { getPizzas } from 'src/api/pizzasApi';
import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';

import { Pizza } from './Pizza';

export const PizzaList = () => {
  const { root } = pizzaListStyles();

  const [pizzas, setPizzas] = useState<IPizza[]>([]);
  useEffect(() => {
    const getPizzasList = async () => {
      const result = await getPizzas();
      setPizzas(result);
      console.log(result);
    };
    getPizzasList();
  }, []);

  return (
    <Grid container justify="center" className={root}>
      <Grid item>
        <Grid container justify="center">
          {pizzas.map(pizza => (
            <Pizza key={pizza.id} pizza={pizza} />
          ))}
        </Grid>
      </Grid>
    </Grid>
  );
};
