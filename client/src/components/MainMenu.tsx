import React, { useEffect } from 'react';

import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';

import { PizzaList } from './PizzaList';

export const MainMenu = () => {
  useEffect(() => {
    const getData = async () => {
      if (menuStore.pizzas.length === 0) {
        await menuStore.loadData();
        await userStore.loadData();
      }
    };
    getData();
  }, []);

  return <PizzaList />;
};
