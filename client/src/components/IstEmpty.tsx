import React, { useState, useEffect } from 'react';
import { Grid, Typography } from '@material-ui/core';
import { observer } from 'mobx-react-lite';

import { IIngredient } from 'src/interfaces/ingredient';
import { getAdditionalIngredients } from 'src/api/additionalIngredientsApi';
import { additionalIngredientsListStyles } from 'src/componentsStyles/additionalIngredientsListStyles';
import { menuStore } from 'src/store/currentMenu';
import { IngredientForPizzaCreating } from 'src/components/IngredientForPizzaCreating';
import { itsEmptyStyles } from 'src/componentsStyles/istEmptyStyles';

export const ItsEmpty = observer(() => {
  const { root } = itsEmptyStyles();

  return (
    <Grid container alignItems="center" justifyContent="center">
      <Grid item>
        <Typography className={root} variant="h4">
          Упс... Здесь пока ничего нет
        </Typography>
      </Grid>
    </Grid>
  );
});
