import { CardContent, Card, CardActionArea, CardMedia, Grid } from '@material-ui/core';
import React, { useState } from 'react';
import { Observer } from 'mobx-react-lite';

import { IIngredientProps } from 'src/interfaces/ingredient';
import { ingredientForCatalogStyles } from 'src/componentsStyles/ingredientForCatalogStyles';
import { IAdditionalIngredientProps } from 'src/interfaces/additionalIngredient';

export const AdditionalIngredientForCatalog = (props: IAdditionalIngredientProps) => {
  const { root, border, media } = ingredientForCatalogStyles();
  const { additionalIngredient } = props;
  const { name, price, imageLink } = additionalIngredient;
  const [picked, setPicked] = useState(false);

  return (
    <Grid container alignItems="center" justify="center">
      <Card className={picked ? `${root} ${border}` : root}>
        <CardMedia image={imageLink} className={media} title="Photo of ingredient" />
      </Card>
    </Grid>
  );
};
