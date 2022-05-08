import { Button, Card, CardMedia, Grid, LinearProgress, TextField } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react-lite';

import { catalogsEdittingStyles } from 'src/componentsStyles/catalogsEdittingStyles';
import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';
import {
  AvialabnessForEditting,
  ButtonForEditting,
  DelButtonForEditting,
  NameForEditting,
  PriceForEditting,
} from 'src/components/AdditionalIngredientsCatalogEditting';

import { PizzaList } from './PizzaList';
import { AdditionalIngredientForCatalog } from './AdditionalIngredientForCatalog';

export const AdditionalIngredientsCatalog = observer(() => {
  const { root, addButton, center, namefieldwidth, pricefieldwidth, button, loadLine1, iconRoot } =
    catalogsEdittingStyles();

  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [cover, setCover] = useState('');
  const getCoverBase64 = (file: Blob) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      setCover(typeof reader.result === 'string' ? reader.result : '');
    };
  };

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
          <h3 className={center}> Иконка </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <AdditionalIngredientForCatalog key={ing.id} additionalIngredient={ing} />,
          )}

          <Grid container alignItems="center" justify="center">
            <Card className={iconRoot}>
              <CardMedia>
                <div
                  onClick={() => {
                    document.getElementById('fileinput')?.click();
                  }}
                >
                  <img
                    src={
                      cover !== ''
                        ? cover
                        : 'https://img2.freepng.ru/20180315/qae/kisspng-computer-icons-plus-sign-clip-art-plus-sign-5aaad899307aa1.3479178215211460091986.jpg'
                    }
                    className={iconRoot}
                  />
                  <input
                    type="file"
                    accept="image/png, image/jpeg"
                    style={{ display: 'none' }}
                    id="fileinput"
                    onChange={async e => {
                      const { files } = e.target;
                      if (files && files.length > 0) {
                        const file = files[0];
                        getCoverBase64(file);
                      }
                    }}
                  />
                </div>
              </CardMedia>
            </Card>
          </Grid>
        </Grid>

        <Grid item xs={2}>
          <h3 className={center}> Добавка </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <NameForEditting key={ing.id} additionalIngredient={ing} />,
          )}

          <TextField
            id="name"
            color="secondary"
            fullWidth
            label="Название"
            className={namefieldwidth}
            onChange={e => setName(e.target.value)}
          />
        </Grid>
        <Grid item xs={2}>
          <h3 className={center}> Цена добавки </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <PriceForEditting key={ing.id} additionalIngredient={ing} />,
          )}
          <TextField
            id="name"
            color="secondary"
            fullWidth
            label="Цена"
            className={pricefieldwidth}
            onChange={e => setPrice(Number(e.target.value))}
          />
        </Grid>
        <Grid item xs={2}>
          <h3 className={center}> Редактирование </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <ButtonForEditting key={ing.id} additionalIngredient={ing} />,
          )}

          <Button
            variant="contained"
            color="primary"
            className={button}
            onClick={() => menuStore.createAdditionalIngredient(name, price, cover)}
          >
            {' '}
            Добавить новую добавку{' '}
          </Button>
        </Grid>
        <Grid item xs={2}>
          <h3 className={center}> Наличие </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <AvialabnessForEditting key={ing.id} additionalIngredient={ing} />,
          )}
        </Grid>
        <Grid item xs={1}>
          <h3 className={center}> Удаление </h3>

          {menuStore.additionalIngredients.map(
            ing => !ing.isDeleted && <DelButtonForEditting key={ing.id} additionalIngredient={ing} />,
          )}
        </Grid>
      </Grid>
    </div>
  );
});
