import React from 'react';
import { observer } from 'mobx-react-lite';
import { Grid, Typography } from '@material-ui/core';

import { IAdditionalIngredientProps } from 'src/interfaces/additionalIngredient';

export const AdditionalIngredientInBasket = observer((props: IAdditionalIngredientProps) => {
  const { additionalIngredient } = props;
  const { id, name, price, imageLink } = additionalIngredient;

  return (
    <Grid>
      <Typography>{name}</Typography>
    </Grid>
  );
});
