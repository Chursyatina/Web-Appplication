import { Button, Grid, LinearProgress, TextField } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';

import { catalogsEdittingStyles } from 'src/componentsStyles/catalogsEdittingStyles';
import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';
import {
  ButtonForEditting,
  DelButtonForEditting,
  NameForEditting,
  PriceForEditting,
} from 'src/components/AdditionalIngredientsCatalogEditting';

import { PizzaList } from './PizzaList';

export const AdditionalIngredientsCatalog = observer(() => {
  const { root, addButton, center, fieldwidth, button, loadLine1 } = catalogsEdittingStyles();

  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);

  if (menuStore.additionalIngredients === null) {
    return (
      <>
        {' '}
        <div className={loadLine1}>
          <LinearProgress color="primary" />
        </div>
      </>
    );
  }
  return (
    <div>
      <h2 className={root}>Добавки</h2>

      <Grid container>
        <Grid item xs={3}>
          <h3 className={center}> Название добавки </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <NameForEditting key={ing.id} additionalIngredient={ing} />,
          )}

          <TextField
            id="name"
            color="secondary"
            fullWidth
            label="Название"
            className={fieldwidth}
            onChange={e => setName(e.target.value)}
          />
        </Grid>
        <Grid item xs={3}>
          <h3 className={center}> Цена добавки </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <PriceForEditting key={ing.id} additionalIngredient={ing} />,
          )}
          <TextField
            id="name"
            color="secondary"
            fullWidth
            label="Цена"
            className={fieldwidth}
            onChange={e => setPrice(Number(e.target.value))}
          />
        </Grid>
        <Grid item xs={3}>
          <h3 className={center}> Редактирование </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <ButtonForEditting key={ing.id} additionalIngredient={ing} />,
          )}

          <Button
            variant="contained"
            color="primary"
            className={button}
            onClick={() => menuStore.createAdditionalIngredient(name, price, 'some')}
          >
            {' '}
            Добавить новую доюавку{' '}
          </Button>
        </Grid>
        <Grid item xs={3}>
          <h3 className={center}> Удаление </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <DelButtonForEditting key={ing.id} additionalIngredient={ing} />,
          )}
        </Grid>
      </Grid>
    </div>
  );
});