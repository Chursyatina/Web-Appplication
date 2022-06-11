import { Grid, Typography } from '@material-ui/core';
import { observer } from 'mobx-react-lite';
import React from 'react';

import { OrderForHistory } from 'src/components/OrderForHistory';
import { orderHistoryStyles } from 'src/componentsStyles/orderHistoryStyles';
import { ordersStore } from 'src/store/currentOrders';

import { ItsEmpty } from './IstEmpty';
import { OrderDatePicker } from './OrderDatePicker';
import { PizzaInBasketList } from './PizzaInBasketList';

export const OrdersHistory = observer(() => {
  const { typo } = orderHistoryStyles();

  return (
    <div>
      <Typography variant="h4" align="center" className={typo}>
        История заказов
      </Typography>
      <Grid container alignItems="center">
        <Grid item justifyContent="center" alignItems="center" xs={12}>
          <OrderDatePicker />
        </Grid>
        {ordersStore.filteredOrders.length !== 0 ? (
          <Grid container justify="center" alignItems="center">
            {ordersStore.filteredOrders.map(order => (
              <OrderForHistory key={order.id} order={order} />
            ))}
          </Grid>
        ) : (
          <ItsEmpty />
        )}
      </Grid>
    </div>
  );
});
