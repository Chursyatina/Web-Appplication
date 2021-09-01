import React, { useState, useEffect } from 'react';
import { Paper, Tab, Tabs } from '@material-ui/core';

import { getDoughs } from 'src/api/doughsApi';
import { doughTabsStyles } from 'src/componentsStyles/doughTabsStyles';
import { IPizzaDough } from 'src/interfaces/pizzaDough';
import { pizzaStore } from 'src/store/currentPizza';

export const DoughTabs = () => {
  const { root } = doughTabsStyles();
  const [doughs, setDoughs] = useState<IPizzaDough[]>([]);
  const [value, setValue] = useState(0);

  useEffect(() => {
    const getDoughsList = async () => {
      const result = await getDoughs();
      setDoughs(result);
      pizzaStore.setDough(result[0]);
    };
    getDoughsList();
  }, []);

  const handleChange = (event: React.ChangeEvent<unknown>, newValue: number) => {
    setValue(newValue);
    pizzaStore.setDough(doughs[newValue]);
  };

  return (
    <Paper className={root}>
      <Tabs value={value} onChange={handleChange} indicatorColor="primary" textColor="primary" centered>
        {doughs.map(dough => (
          <Tab label={dough.name} key={dough.id} />
        ))}
      </Tabs>
    </Paper>
  );
};
