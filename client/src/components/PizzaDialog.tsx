import React, { useState } from 'react';
import { Grid, Typography, Button, Modal, Box } from '@material-ui/core';
import { Observer } from 'mobx-react-lite';

import { IPizzaProps } from 'src/interfaces/pizza';
import { SizeTabs } from 'src/components/SizeTabs';
import { DoughTabs } from 'src/components/DoughTabs';
import { AdditionalIngredientsList } from 'src/components/AdditionalIngredientsList';
import { pizzaDialogStyles } from 'src/componentsStyles/pizzaDialogStyles';
import { pizzaStore } from 'src/store/currentPizza';
import { userStore } from 'src/store/currentUser';

import { IngredientListWithDeleteButton } from './IngredientListWithDeleteButton';

export const ButtonForEditting = (props: IPizzaProps) => {
  const { button, center, paper, pizzaImage, basketItem, buyButton, tab } = pizzaDialogStyles();
  const { pizza } = props;
  const { singleItemImageLink } = pizza;

  const [open, setOpen] = useState(false);

  const clickHandler = () => {
    setOpen(!open);
    pizzaStore.setPizza(pizza);
  };

  const body = (
    <div className={paper}>
      <Grid container>
        <Grid item xs={7} className={pizzaImage}>
          <img srcSet={singleItemImageLink} width="539" height="500" />
        </Grid>
        <Grid item xs={5} className={basketItem}>
          <Typography gutterBottom variant="h5" align="center">
            {pizzaStore.pizza.name}
          </Typography>
          <Typography variant="body2" color="textSecondary">
            {pizzaStore.pizza.description}
          </Typography>
          <Grid container>
            <Grid item xs={12}>
              <IngredientListWithDeleteButton pizza={pizzaStore.pizza} />
            </Grid>
          </Grid>
          <div className={tab}>
            <Typography variant="h6">Size</Typography>
            <SizeTabs />
          </div>
          <div className={tab}>
            <Typography variant="h6">Dough</Typography>
            <DoughTabs />
          </div>
          <div>
            <Typography variant="h6">Additional ingredients</Typography>
            <AdditionalIngredientsList />
          </div>
        </Grid>
        <Grid item xs={12}>
          <Grid container justify="flex-end">
            <Observer>
              {() => (
                <Button
                  variant="contained"
                  color="primary"
                  className={buyButton}
                  onClick={() => {
                    userStore.addCurrentPizzaToBasket();
                    setOpen(!open);
                  }}
                >
                  {pizzaStore.pizza.discount !== 0 ? (
                    <Box>
                      Купить за <s>{pizzaStore.price}</s>{' '}
                      {Number(pizzaStore.price - pizzaStore.price * pizzaStore.pizza.discount).toFixed(3)} ₽
                    </Box>
                  ) : (
                    <Box>Купить за {pizzaStore.price} ₽</Box>
                  )}
                </Button>
              )}
            </Observer>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );

  return (
    <div>
      <div className={center}>
        <Button className={button} onClick={clickHandler}>
          Купить
        </Button>
      </div>
      <Modal
        open={open}
        onClose={clickHandler}
        aria-labelledby="simple-modal-title"
        aria-describedby="simple-modal-description"
      >
        {body}
      </Modal>
    </div>
  );
};
