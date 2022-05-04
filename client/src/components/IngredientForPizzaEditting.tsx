import { CardContent, Card, CardActionArea, CardMedia } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { Observer } from 'mobx-react-lite';

import { IIngredientIsPickedProps, IIngredientProps } from 'src/interfaces/ingredient';
import { additionalIngredientStyles } from 'src/componentsStyles/additionalIngredientStyles';
import { pizzaStore } from 'src/store/currentPizza';
import { creatingPizzaStore } from 'src/store/creatingPizza';

export const IngredientForPizzaEditting = (props: IIngredientIsPickedProps) => {
  const { root, border, media, ingredientName } = additionalIngredientStyles();
  const { ingredient } = props;
  const { name, price, imageLink } = ingredient;
  const [picked, setPicked] = useState(false);

  useEffect(() => {
    const setIfPicked = () => {
      if (props.isPicked) {
        setPicked(!picked);
      }
    };
    setIfPicked();
  }, []);

  return (
    <Card
      className={picked ? `${root} ${border}` : root}
      onClick={() => {
        setPicked(!picked);
        creatingPizzaStore.changeExistenceOfIngredient(ingredient);
      }}
    >
      <CardActionArea>
        <CardMedia image={imageLink} className={media} title="Photo of ingredient" />
        <CardContent>
          <div className={ingredientName}>{name}</div>
          <Observer>{() => <div>{`Price: ${price}`}</div>}</Observer>
        </CardContent>
      </CardActionArea>
    </Card>
  );
};
