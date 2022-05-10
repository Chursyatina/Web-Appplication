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

export const OrderDatePicker = () => {
  const [startValue, setStartValue] = React.useState<Date | null>(null);
  const [endValue, setEndValue] = React.useState<Date | null>(null);

  const { datePicker } = datePickersStyles();

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns} locale={ruLocale}>
      <Grid container alignItems="center" justifyContent="center">
        <Grid item justifyContent="center" alignItems="center" className={datePicker}>
          <DatePicker
            mask="__.__.____"
            label="Дата начала"
            value={startValue}
            onChange={newValue => {
              setStartValue(newValue);
              ordersStore.filterOrders(newValue, endValue);
            }}
            renderInput={params => <TextField {...params} />}
          />
        </Grid>
        <Grid item justifyContent="center" alignItems="center" className={datePicker}>
          <DatePicker
            mask="__.__.____"
            label="Дата окончания"
            value={endValue}
            onChange={newValue => {
              setEndValue(newValue);
              ordersStore.filterOrders(startValue, newValue);
            }}
            renderInput={params => <TextField {...params} />}
          />
        </Grid>
      </Grid>
    </LocalizationProvider>
  );
};
