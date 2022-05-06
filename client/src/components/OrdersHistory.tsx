import { Grid, Typography } from '@material-ui/core';
import { observer } from 'mobx-react-lite';
import React from 'react';

import { OrderForHistory } from 'src/components/OrderForHistory';
import { orderHistoryStyles } from 'src/componentsStyles/orderHistoryStyles';
import { ordersStore } from 'src/store/currentOrders';

import { PizzaInBasketList } from './PizzaInBasketList';

export const OrdersHistory = observer(() => {
  const { typo } = orderHistoryStyles();

  return (
    <div>
      <Typography variant="h4" align="center" className={typo}>
        История заказов
      </Typography>
      <Grid container>
        <Grid container justify="center" alignItems="center">
          {ordersStore.orders.map(order => (
            <OrderForHistory key={order.id} order={order} />
          ))}
        </Grid>
      </Grid>
    </div>
  );
});
