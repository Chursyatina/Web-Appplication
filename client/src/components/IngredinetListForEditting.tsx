import React, { useState, useEffect } from 'react';
import { Grid } from '@material-ui/core';
import { observer } from 'mobx-react-lite';

import { IIngredient } from 'src/interfaces/ingredient';
import { getAdditionalIngredients } from 'src/api/additionalIngredientsApi';
import { additionalIngredientsListStyles } from 'src/componentsStyles/additionalIngredientsListStyles';
import { menuStore } from 'src/store/currentMenu';
import { IngredientForPizzaEditting } from 'src/components/IngredientForPizzaEditting';
import { IPizzaProps } from 'src/interfaces/pizza';

export const IngredientsListForEditting = observer((props: IPizzaProps) => {
  const { root } = additionalIngredientsListStyles();

  return (
    <div>
      <Grid container justify="center" className={root}>
        <Grid item>
          <Grid container justify="center">
            {menuStore.ingredients.map(
              ingredient =>
                !ingredient.isDeleted && (
                  <IngredientForPizzaEditting
                    key={ingredient.id}
                    ingredient={ingredient}
                    isPicked={props.pizza.ingredients.findIndex(ing => ing.id === ingredient.id) === -1 ? false : true}
                  />
                ),
            )}
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
});
