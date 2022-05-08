import { CardContent, Typography, Card, CardActionArea, CardMedia, Grid, CardActions, Button } from '@material-ui/core';
import React from 'react';

import { IPizzaProps } from 'src/interfaces/pizza';
import { pizzaStyles } from 'src/componentsStyles/pizzaStyles';
import { userStore } from 'src/store/currentUser';
import { menuStore } from 'src/store/currentMenu';

import { ButtonForEditting } from './PizzaDialog';
import { EdittingPizzaDialog } from './EdittingPizzaDialog';

export const Pizza = (props: IPizzaProps) => {
  const { button, root, media, pizzaDescription, cardActions, divBack, divBackAdmin, notAvialable } = pizzaStyles();
  const { pizza } = props;
  const { id, imageLink, name, description, price } = pizza;

  return (
    <Card className={root}>
      <CardActionArea>
        <CardMedia image={imageLink} className={media} title="Photo of pizza" />
      </CardActionArea>
      <CardContent>
        <Typography gutterBottom variant="h5" component="h2">
          {name}
        </Typography>
        <Typography variant="body2" color="textSecondary" component="p" className={pizzaDescription}>
          {description}
        </Typography>
      </CardContent>
      <CardActions disableSpacing className={cardActions}>
        <Grid container justify="flex-start">
          <Typography variant="h6" component="h2">
            {`from ${price} $`}
          </Typography>
        </Grid>
        {userStore.role === 'admin' ? (
          <Grid container justify="flex-end">
            <Button className={button} onClick={() => menuStore.deletePizza(id)}>
              Delete
            </Button>
            <EdittingPizzaDialog pizza={pizza} />
          </Grid>
        ) : (
          <Grid container justify="flex-end">
            <ButtonForEditting pizza={pizza} />
          </Grid>
        )}
      </CardActions>
      {!pizza.isAvailable && userStore.role === 'admin' && (
        <div className={divBackAdmin}>
          <Typography variant="h4" className={notAvialable}>
            {' '}
            Нет в наличии{' '}
          </Typography>
        </div>
      )}
    </Card>
  );
};
