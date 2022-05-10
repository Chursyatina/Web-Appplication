import * as React from 'react';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { Grid } from '@material-ui/core';
import ruLocale from 'date-fns/locale/ru';

import { datePickersStyles } from 'src/componentsStyles/datePickersStyles';
import { ordersStore } from 'src/store/currentOrders';
import { IIngredient, IIngredientProps } from 'src/interfaces/ingredient';
import { IAdditionalIngredient, IAdditionalIngredientProps } from 'src/interfaces/additionalIngredient';
import { menuStore } from 'src/store/currentMenu';

export const IngredientSearchBar = () => {
  const [value, setValue] = React.useState(null);

  return (
    <TextField
      id="outlined-basic"
      label="Поиск по названию"
      variant="outlined"
      onChange={e => {
        menuStore.filterIngredients(e.target.value);
      }}
    />
  );
};

export const AdditionalIngredientSearchBar = () => {
  const [value, setValue] = React.useState(null);

  return (
    <TextField
      id="outlined-basic"
      label="Поиск по названию"
      variant="outlined"
      onChange={e => {
        console.log(e.target.value);
        menuStore.filterAdditionalIngredients(e.target.value);
      }}
    />
  );
};
