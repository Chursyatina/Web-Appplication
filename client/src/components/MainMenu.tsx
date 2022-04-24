import React, { useEffect } from 'react';

import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';

import { PizzaList } from './PizzaList';

export const MainMenu = () => {
  useEffect(() => {
    const getData = async () => {
      if (menuStore.pizzas.length === 0) {
        console.log(menuStore.pizzas.length);
        console.log(Date.now());
        console.log('started loading');
        await menuStore.loadData();
        await userStore.loadData();
        console.log('loaded');
        console.log(userStore.basket.orderLines.length);
      }
    };
    getData();
  }, []);

  return <PizzaList />;
};
