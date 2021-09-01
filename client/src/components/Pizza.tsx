import { CardContent, Typography, Card, CardActionArea, CardMedia, Grid, CardActions } from '@material-ui/core';
import React from 'react';

import { IPizzaProps } from 'src/interfaces/pizza';
import { pizzaStyles } from 'src/componentsStyles/pizzaStyles';

import { ButtonForEditting } from './PizzaDialog';

export const Pizza = (props: IPizzaProps) => {
  const { root, media, pizzaDescription, cardActions } = pizzaStyles();
  const { pizza } = props;
  const { imageLink, name, description, price } = pizza;

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
        <Grid container justify="flex-end">
          <ButtonForEditting pizza={pizza} />
        </Grid>
      </CardActions>
    </Card>
  );
};
