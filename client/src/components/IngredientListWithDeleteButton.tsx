import React from 'react';
import { List } from '@material-ui/core';

import { ingredientListStyles } from 'src/componentsStyles/ingredientListStyles';
import { IPizzaProps } from 'src/interfaces/pizza';

import { IngredientWithDeleteButton } from './IngredientWithDeleteButton';

export const IngredientListWithDeleteButton = (props: IPizzaProps) => {
  const { withNoMarginsAndPaddings } = ingredientListStyles();

  return (
    <List className={withNoMarginsAndPaddings}>
      <div className={withNoMarginsAndPaddings}>
        {props.pizza.ingredients.map(
          ingredient =>
            !ingredient.isDeleted && <IngredientWithDeleteButton key={ingredient.id} ingredient={ingredient} />,
        )}
      </div>
    </List>
  );
};
