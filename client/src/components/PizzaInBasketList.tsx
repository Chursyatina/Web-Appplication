import React, { useEffect, useState } from 'react';
import { Grid } from '@material-ui/core';
import { observer } from 'mobx-react-lite';

import { IPizzaVariation } from 'src/interfaces/pizzaVariation';
import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';
import { getOrder } from 'src/api/ordersApi';
import { basketStore } from 'src/store/currentBasket';
import { IOrder } from 'src/interfaces/order';

import { PizzaInBasket } from './PizzaInBasket';

export const PizzaInBasketList = observer(() => {
  const { root } = pizzaListStyles();

  const [order, setOrder] = useState<IOrder>({} as IOrder);

  useEffect(() => {
    basketStore.loadData(1);

    // const getSpecificOrder = async (id: number) => {
    //   const result = await getOrder(id);
    //   setOrder(result);
    //   console.log(result);
    //   basketStore.setBasket(result);
    //   console.log(basketStore.orderLines);
    // };
    // getSpecificOrder(1);
  }, []);

  return (
    <Grid container justify="center" className={root}>
      {basketStore.order.orderLines.map(orderLine => (
        <Grid item key={orderLine.id}>
          <PizzaInBasket orderLine={orderLine} />
        </Grid>
      ))}
    </Grid>
  );
});
