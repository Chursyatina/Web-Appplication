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
} from 'src/components/DoughsCatalogEditting';
import { sizesDoughsCatalogsStyles } from 'src/componentsStyles/sizesdoughsCatalogsStyles';

import { PizzaList } from './PizzaList';

export const DoughsCatalog = observer(() => {
  const { root, addButton, center, fieldwidth, button, loadLine1 } = sizesDoughsCatalogsStyles();

  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);

  const [isNameError, setNameError] = useState(false);
  const [isPriceError, setPriceError] = useState(false);

  if (menuStore.doughs === null) {
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
      <h2 className={root}>Типы теста</h2>

      <Grid container>
        <Grid item xs={3}>
          <h3 className={center}> Наименование </h3>

          {menuStore.doughs.map(d => !d.isDeleted && <NameForEditting key={d.id} dough={d} />)}

          {!isNameError ? (
            <TextField
              autoFocus
              margin="dense"
              id="ingname"
              label="Название"
              type="email"
              fullWidth
              value={name}
              onChange={e => setName(e.target.value)}
            />
          ) : (
            <TextField
              error
              autoFocus
              margin="dense"
              id="ingname"
              label="Название"
              type="email"
              fullWidth
              value={name}
              helperText="Строка от 1 до 20 символов"
              onChange={e => setName(e.target.value)}
            />
          )}
        </Grid>
        <Grid item xs={3}>
          <h3 className={center}> Множитель цены ингредиентов </h3>

          {menuStore.doughs.map(d => !d.isDeleted && <PriceForEditting key={d.id} dough={d} />)}
          {!isPriceError ? (
            <TextField
              id="name"
              color="secondary"
              fullWidth
              label="Множитель цены"
              className={fieldwidth}
              onChange={e => setPrice(Number(e.target.value))}
            />
          ) : (
            <TextField
              error
              id="name"
              color="secondary"
              fullWidth
              label="Множитель цены"
              className={fieldwidth}
              helperText="Число от 0.1 до 3"
              onChange={e => setPrice(Number(e.target.value))}
            />
          )}
        </Grid>
        <Grid item xs={3}>
          <h3 className={center}> Редактирование </h3>

          {menuStore.doughs.map(d => !d.isDeleted && <ButtonForEditting key={d.id} dough={d} />)}

          <Button
            variant="contained"
            color="primary"
            className={button}
            onClick={() => {
              setNameError(false);
              setPriceError(false);
              if (name.length < 1 || name.length > 20) {
                setNameError(true);
              } else if (
                isNaN(Number(price.toString())) ||
                Number(price.toString()) < 0.1 ||
                Number(price.toString()) > 3
              ) {
                setPriceError(true);
              } else {
                menuStore.createDough(name, price);
              }
            }}
          >
            {' '}
            Добавить новый тип теста{' '}
          </Button>
        </Grid>
        <Grid item xs={3}>
          <h3 className={center}> Удаление </h3>

          {menuStore.doughs.map(d => !d.isDeleted && <DelButtonForEditting key={d.id} dough={d} />)}
        </Grid>
      </Grid>
    </div>
  );
});
