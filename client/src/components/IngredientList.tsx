import React from 'react';
import { List, ListItem, ListItemIcon, ListItemText } from '@material-ui/core';
import LocalPizzaIcon from '@material-ui/icons/LocalPizza';

import { ingredientListStyles } from 'src/componentsStyles/ingredientListStyles';
import { IPizzaProps } from 'src/interfaces/pizza';

export const IngredientList = (props: IPizzaProps) => {
  const { withNoMarginsAndPaddings } = ingredientListStyles();

  return (
    <List className={withNoMarginsAndPaddings}>
      <div className={withNoMarginsAndPaddings}>
        {props.pizza.ingredients.map(ingredient => (
          <ListItem key={ingredient.id} className={withNoMarginsAndPaddings}>
            <ListItemIcon>
              <LocalPizzaIcon />
            </ListItemIcon>
            <ListItemText primary={`${ingredient.name}`} />
          </ListItem>
        ))}
      </div>
    </List>
  );
};
