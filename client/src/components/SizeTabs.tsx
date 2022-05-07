import React, { useState, useEffect } from 'react';
import { Paper, Tab, Tabs } from '@material-ui/core';

import { getSizes } from 'src/api/sizesApi';
import { sizeTabsStyles } from 'src/componentsStyles/sizeTabsStyles';
import { IPizzaSize } from 'src/interfaces/pizzaSize';
import { pizzaStore } from 'src/store/currentPizza';
import { userStore } from 'src/store/currentUser';
import { menuStore } from 'src/store/currentMenu';

export const SizeTabs = () => {
  const { root, tab } = sizeTabsStyles();
  const [sizes, setSizes] = useState<IPizzaSize[]>([]);
  const [value, setValue] = useState(0);

  useEffect(() => {
    const getChosenSize = async () => {
      setValue(menuStore.sizes.findIndex(size => size.id === pizzaStore.size.id));
    };
    getChosenSize();
  }, []);

  const handleChange = (event: React.ChangeEvent<unknown>, newValue: number) => {
    setValue(newValue);
    pizzaStore.setSize(menuStore.sizes[newValue]);
  };

  return (
    <Paper className={root}>
      <Tabs value={value} onChange={handleChange} indicatorColor="primary" textColor="primary" centered>
        {menuStore.sizes.map(size => (
          <Tab label={size.name} key={size.id} classes={{ root: tab }} />
        ))}
      </Tabs>
    </Paper>
  );
};
