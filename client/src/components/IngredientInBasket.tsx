import React from 'react';
import { observer } from 'mobx-react-lite';
import { Grid, Typography } from '@material-ui/core';

import { IIngredientProps } from 'src/interfaces/ingredient';

export const IngredientInBasket = observer((props: IIngredientProps) => {
  const { ingredient } = props;
  const { id, name, price, imageLink } = ingredient;

  return (
    <Grid>
      <Typography>{name}</Typography>
    </Grid>
  );
});
