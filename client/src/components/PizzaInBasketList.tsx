import React, { useEffect, useState } from 'react';
import { Grid, Divider, Typography, Button } from '@material-ui/core';
import { observer } from 'mobx-react-lite';

import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';
import { userStore } from 'src/store/currentUser';
import { IOrder } from 'src/interfaces/order';

import { PizzaInBasket } from './PizzaInBasket';

export const PizzaInBasketList = observer(() => {
  const { root, summary, button } = pizzaListStyles();

  const [order, setOrder] = useState<IOrder>({} as IOrder);

  return (
    <div className={root}>
      <Grid container justify="center">
        {userStore.basket.orderLines.map(orderLine => (
          <Grid item key={orderLine.id}>
            <PizzaInBasket orderLine={orderLine} />
          </Grid>
        ))}
      </Grid>
      <Divider variant="middle" />
      <Grid container justify="flex-end" className={summary}>
        <Grid item>
          <Typography variant="h5" component="h5">
            {`Итого: `} {userStore.basket.orderLines.length} {` позиции`}
          </Typography>
          <Typography variant="h5" component="h5">
            {`Сумма заказа: `} {userStore.basket.price} {` ₽`}
          </Typography>
          <Button
            className={button}
            onClick={() => {
              userStore.createOrder(), userStore.clearBasket();
            }}
            variant="contained"
            color="primary"
            fullWidth
          >{`Оформить заказ`}</Button>
        </Grid>
      </Grid>
    </div>
  );
});
