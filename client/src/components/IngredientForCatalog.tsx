import { CardContent, Card, CardActionArea, CardMedia, Grid } from '@material-ui/core';
import React, { useState } from 'react';
import { Observer } from 'mobx-react-lite';

import { IIngredientProps } from 'src/interfaces/ingredient';
import { ingredientForCatalogStyles } from 'src/componentsStyles/ingredientForCatalogStyles';

export const IngredientForCatalog = (props: IIngredientProps) => {
  const { root, border, media } = ingredientForCatalogStyles();
  const { ingredient } = props;
  const { name, price, imageLink } = ingredient;
  const [picked, setPicked] = useState(false);

  return (
    <Grid container alignItems="center" justify="center">
      <Card className={picked ? `${root} ${border}` : root}>
        <CardMedia image={imageLink} className={media} title="Photo of ingredient" />
      </Card>
    </Grid>
  );
};
