import React from 'react';
import { Card, CardActionArea, CardContent, Grid, IconButton, Typography } from '@material-ui/core';
import { AddBoxOutlined, IndeterminateCheckBoxOutlined, Delete } from '@material-ui/icons';

import { pizzaInBasketStyles } from 'src/componentsStyles/pizzaInBasketStyles';
import { IPizzaVariationProps } from 'src/interfaces/pizzaVariation';
import { IOrderLineProps } from 'src/interfaces/orderLine';

export const PizzaInBasket = (props: IOrderLineProps) => {
  const { orderLine } = props;
  const { id, pizzaVariation, price, quantity } = props.orderLine;

  const { root, cover } = pizzaInBasketStyles();

  return (
    <Card className={root}>
      <CardActionArea
        onClick={() => {
          document.location.href = `/${pizzaVariation.id}`;
        }}
      >
        <CardContent>
          <Grid container spacing={3}>
            <Grid item xs={2} md={1}>
              <img className={cover} src={pizzaVariation.pizza.singleItemImageLink} />
            </Grid>
            <Grid item xs={7} md={7}>
              <Typography variant="h5" component="h5">
                {pizzaVariation.pizza.name}
              </Typography>
              <Typography>{pizzaVariation.pizza.description}</Typography>
            </Grid>
            <Grid container xs={3} md={1} justify="center" alignItems="center">
              <Grid item xs={4} md={4}>
                <IconButton
                  onClick={e => {
                    // deleteGameToBasket(game.id || '');
                    e.stopPropagation();
                  }}
                >
                  <IndeterminateCheckBoxOutlined />
                </IconButton>
              </Grid>
              <Grid item xs={4} md={4}>
                <Typography variant="h6" component="h6" align="center">
                  {orderLine.quantity}
                </Typography>
              </Grid>
              <Grid item xs={4} md={4}>
                <IconButton
                  onClick={e => {
                    // deleteGameToBasket(game.id || '');
                    e.stopPropagation();
                  }}
                >
                  <AddBoxOutlined />
                </IconButton>
              </Grid>
            </Grid>
            <Grid item container xs={3} md={2} justify="center" alignItems="center" direction="column">
              <Typography variant="h6" component="h6">
                {`Итого:`}
              </Typography>
              <Typography variant="h6" component="h6">
                {orderLine.price}
              </Typography>
            </Grid>
            <Grid item container xs={2} md={1} justify="center" alignItems="center">
              <Grid item>
                <IconButton
                  onClick={e => {
                    // deleteGameToBasket(game.id || '');
                    e.stopPropagation();
                  }}
                >
                  <Delete />
                </IconButton>
              </Grid>
            </Grid>
          </Grid>
        </CardContent>
      </CardActionArea>
    </Card>
  );
};
