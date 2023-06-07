import React, { useState, useEffect } from 'react';
import { Grid } from '@material-ui/core';
import { observer } from 'mobx-react-lite';

import { IPizza } from 'src/interfaces/pizza';
import { getPizzas } from 'src/api/pizzasApi';
import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';
import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';
import { AddingNewPizzaDialog } from 'src/components/AddingNewPizzaDialog';

import { Pizza } from './Pizza';

export const PizzaList = observer(() => {
  const { root } = pizzaListStyles();

  const [pizzas, setPizzas] = useState<IPizza[]>([]);

  useEffect(() => {
    // const getPizzasList = async () => {
    //   const result = await getPizzas();
    //   setPizzas(result);
    //   console.log(result);
    // };
    // getPizzasList();

    console.log('getting menu from store');
  }, []);

  return (
    <Grid container justify="center" className={root}>
      <Grid item>
        {userStore.role === 'admin' ? (
          <Grid container justify="center">
            {menuStore.pizzas.map(pizza => (
              <Pizza key={pizza.id} pizza={pizza} />
            ))}
            {userStore.role === 'admin' && <AddingNewPizzaDialog />}
          </Grid>
        ) : (
          <Grid container justify="center">
            {menuStore.pizzas.map(pizza => pizza.isAvailable && <Pizza key={pizza.id} pizza={pizza} />)}
          </Grid>
        )}
      </Grid>
    </Grid>
  );
});
