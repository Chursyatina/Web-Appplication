import React, { useState, useEffect } from 'react';
import { Grid } from '@material-ui/core';

import { IIngredient } from 'src/interfaces/ingredient';
import { getAdditionalIngredients } from 'src/api/additionalIngredientsApi';
import { additionalIngredientsListStyles } from 'src/componentsStyles/additionalIngredientsListStyles';
import { menuStore } from 'src/store/currentMenu';

import { AdditionalIngredient } from './AdditionalIngredient';

export const AdditionalIngredientsList = () => {
  const { root } = additionalIngredientsListStyles();

  return (
    <div>
      <Grid container justify="center" className={root}>
        <Grid item>
          <Grid container justify="center">
            {menuStore.ingredients.map(
              ingredient =>
                !ingredient.isDeleted && <AdditionalIngredient key={ingredient.id} ingredient={ingredient} />,
            )}
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};
