import React from 'react';
import { Grid, IconButton } from '@material-ui/core';
import { Delete } from '@material-ui/icons';

import { IPizzaVariationProps } from 'src/interfaces/pizzaVariation';

export const PizzaInBasket = (props: IPizzaVariationProps) => {
  const { pizzaVariation } = props;
  const { price } = pizzaVariation;

  return (
    <Grid container>
      <Grid item xs={2} />
      <Grid item xs={5} />
      <Grid item xs={2} />
      <Grid item xs={2}>
        {`${price} $`}
      </Grid>
      <Grid item xs={1}>
        <IconButton edge="end" aria-label="delete">
          <Delete />
        </IconButton>
      </Grid>
    </Grid>
  );
};
