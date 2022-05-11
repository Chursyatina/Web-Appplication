import React, { useState } from 'react';
import { Card, CardActionArea, CardContent, Divider, Grid, IconButton, Typography } from '@material-ui/core';
import { AddBoxOutlined, IndeterminateCheckBoxOutlined, Delete } from '@material-ui/icons';
import { observer } from 'mobx-react-lite';

import { pizzaInBasketStyles } from 'src/componentsStyles/pizzaInBasketStyles';
import { userStore } from 'src/store/currentUser';
import { IPizzaVariationProps } from 'src/interfaces/pizzaVariation';
import { IOrderLineProps } from 'src/interfaces/orderLine';
import { IngredientInBasket } from 'src/components/IngredientInBasket';

import { PizzaInBasketEditting } from './PizzaInBasketEditting';
import { AdditionalIngredientInBasket } from './AddtionalIngredientInBasket';

export const PizzaInBasket = observer((props: IOrderLineProps) => {
  const { orderLine } = props;
  const { id, pizzaVariation, price, quantity } = props.orderLine;

  const [open, setOpen] = useState(false);

  const { root, cover } = pizzaInBasketStyles();

  return (
    <div>
      <Card className={root}>
        <CardActionArea
          onClick={() => {
            console.log(open);
            setOpen(!open);
          }}
        >
          <CardContent>
            <Grid container spacing={3}>
              <Grid item xs={2} md={1}>
                <img className={cover} src={pizzaVariation.pizza.singleItemImageLink} />
              </Grid>
              <Grid container item xs={7} md={7} direction="column">
                <Typography variant="h5" component="h5">
                  {pizzaVariation.pizza.name}
                </Typography>
                <Typography>{pizzaVariation.pizza.description}</Typography>
                <Grid container>
                  <Grid item xs={6} md={6}>
                    <Typography
                      variant="subtitle2"
                      style={{ fontWeight: 600 }}
                    >{`Дополнительные ингредиеты`}</Typography>
                    {pizzaVariation.additionalIngredients.map(ingredient => (
                      <AdditionalIngredientInBasket key={ingredient.id} additionalIngredient={ingredient} />
                    ))}
                  </Grid>
                  <Grid item xs={6} md={6}>
                    <Typography variant="subtitle2" style={{ fontWeight: 600 }}>{`Ингредиеты`}</Typography>
                    {pizzaVariation.ingredients.map(ingredient => (
                      <IngredientInBasket key={ingredient.id} ingredient={ingredient} />
                    ))}
                  </Grid>
                </Grid>
              </Grid>
              <Grid container xs={3} md={1} justify="center" alignItems="center">
                <Grid item xs={4} md={4}>
                  <IconButton
                    onClick={e => {
                      if (orderLine.quantity > 0) {
                        userStore.reduceQuantity(orderLine);
                        console.log(props.orderLine.quantity);
                      }
                      e.stopPropagation();
                    }}
                  >
                    <IndeterminateCheckBoxOutlined />
                  </IconButton>
                </Grid>
                <Grid item xs={4} md={4}>
                  <Typography variant="h6" component="h6" align="center">
                    {props.orderLine.quantity}
                  </Typography>
                </Grid>
                <Grid item xs={4} md={4}>
                  <IconButton
                    onClick={e => {
                      if (orderLine.quantity < 4) {
                        userStore.increaseQuantity(orderLine);
                        console.log(props.orderLine.quantity);
                      }
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
                {pizzaVariation.pizza.discount !== 0 && (
                  <Typography variant="h6" component="h6">
                    <s>{Number(price / pizzaVariation.pizza.discount).toFixed(3)}</s>
                  </Typography>
                )}
                <Typography variant="h6" component="h6">
                  {Number(price).toFixed(3)}
                </Typography>
              </Grid>
              <Grid item container xs={2} md={1} justify="center" alignItems="center">
                <Grid item>
                  <IconButton
                    onClick={e => {
                      console.log(userStore.basket.orderLines);
                      userStore.deleteOrderLine(orderLine);
                      console.log(userStore.basket.orderLines);
                      e.stopPropagation();
                    }}
                  >
                    <Delete />
                  </IconButton>
                  <PizzaInBasketEditting pizzaVariation={pizzaVariation} />
                </Grid>
              </Grid>
            </Grid>
          </CardContent>
        </CardActionArea>
      </Card>
    </div>
  );
});
