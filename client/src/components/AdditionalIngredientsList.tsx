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
            {menuStore.additionalIngredients.map(
              ingredient =>
                !ingredient.isDeleted &&
                ingredient.isAvailable && (
                  <AdditionalIngredient key={ingredient.id} additionalIngredient={ingredient} />
                ),
            )}
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};
