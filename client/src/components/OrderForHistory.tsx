import React, { useEffect, useState } from 'react';
import {
  Grid,
  Divider,
  Typography,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  IconButton,
  Input,
  InputAdornment,
  InputLabel,
  TextField,
  Collapse,
  Card,
} from '@material-ui/core';
import { observer } from 'mobx-react-lite';
import MuiPhoneNumber from 'material-ui-phone-number';
import { VisibilityOff, Visibility, ExpandLess, ExpandMore } from '@material-ui/icons';

import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';
import { userStore } from 'src/store/currentUser';
import { IOrder, IOrderProps } from 'src/interfaces/order';

import { PizzaInBasket } from './PizzaInBasket';
import { PizzaInOrderHistory } from './PizzaInOrderHistory';

export const OrderForHistory = observer((props: IOrderProps) => {
  const { root, summary, button } = pizzaListStyles();
  const { order } = props;

  const [open, setOpen] = React.useState(false);
  const [date, setDate] = React.useState('');

  useEffect(() => {
    const parseDate = async () => {
      const newDate = new Date(order.date).toLocaleString('ru');

      setDate(newDate);
      console.log(date);
    };
    parseDate();
  }, []);

  const handleClick = () => {
    setOpen(!open);
  };

  return (
    <Grid item xs={12} justify="center">
      <Card>
        <Button onClick={handleClick}>
          <Typography variant="h6" align="center">
            Заказ на сумму {order.price} рубля, от {date}
          </Typography>
          {open ? <ExpandLess /> : <ExpandMore />}
        </Button>
        <Collapse in={open} timeout="auto" unmountOnExit>
          <Grid container justify="center">
            {order.orderLines.map(orderLine => (
              <Grid item key={orderLine.id}>
                <PizzaInOrderHistory orderLine={orderLine} />
              </Grid>
            ))}
          </Grid>
          <Divider variant="middle" />
          <Grid container justify="flex-end" className={summary}>
            <Grid item>
              <Typography variant="h5" component="h5">
                {`Итого: `} {order.orderLines.length} {` позиции`}
              </Typography>
              <Typography variant="h5" component="h5">
                {`Сумма заказа: `} {order.price} {` ₽`}
              </Typography>
            </Grid>
          </Grid>
        </Collapse>
      </Card>
    </Grid>
  );
});
