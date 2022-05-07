import React, { useEffect, useState } from 'react';
import { IconButton, ListItem, ListItemIcon, ListItemSecondaryAction, ListItemText } from '@material-ui/core';
import { Delete, LocalPizza, RestoreFromTrash } from '@material-ui/icons';

import { ingredientListStyles } from 'src/componentsStyles/ingredientListStyles';
import { IIngredientProps } from 'src/interfaces/ingredient';
import { pizzaStore } from 'src/store/currentPizza';

export const IngredientWithDeleteButton = (props: IIngredientProps) => {
  const { withNoMarginsAndPaddings, crossedText } = ingredientListStyles();
  const { ingredient } = props;
  const [deleted, setDeleted] = useState(false);

  useEffect(() => {
    const checkIffChecked = async () => {
      if (pizzaStore.ingredients.findIndex(ing => ing.id === ingredient.id) === -1) {
        setDeleted(!deleted);
      }
    };
    checkIffChecked();
  }, []);

  return (
    <ListItem key={ingredient.id} className={withNoMarginsAndPaddings}>
      <ListItemIcon>
        <LocalPizza />
      </ListItemIcon>
      <ListItemText className={deleted ? `${crossedText}` : ''} primary={ingredient.name} />
      <ListItemSecondaryAction>
        <IconButton
          edge="end"
          aria-label="delete"
          onClick={() => {
            setDeleted(!deleted);
            pizzaStore.changeExistenceOfIngredient(ingredient);
          }}
        >
          {deleted ? <RestoreFromTrash /> : <Delete />}
        </IconButton>
      </ListItemSecondaryAction>
    </ListItem>
  );
};
