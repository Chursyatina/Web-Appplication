import { CardContent, Card, CardActionArea, CardMedia } from '@material-ui/core';
import React, { useEffect, useState } from 'react';
import { Observer } from 'mobx-react-lite';

import { IIngredientProps } from 'src/interfaces/ingredient';
import { additionalIngredientStyles } from 'src/componentsStyles/additionalIngredientStyles';
import { pizzaStore } from 'src/store/currentPizza';
import { IAdditionalIngredientProps } from 'src/interfaces/additionalIngredient';

export const AdditionalIngredient = (props: IAdditionalIngredientProps) => {
  const { root, border, media, ingredientName } = additionalIngredientStyles();
  const { additionalIngredient } = props;
  const { name, price, imageLink } = additionalIngredient;
  const [picked, setPicked] = useState(false);

  useEffect(() => {
    const checkIffChecked = async () => {
      if (pizzaStore.additionalIngredients.findIndex(ing => ing.id === additionalIngredient.id) !== -1) {
        setPicked(!picked);
      }
    };
    checkIffChecked();
  }, []);

  return (
    <Card
      className={picked ? `${root} ${border}` : root}
      onClick={() => {
        setPicked(!picked);
        pizzaStore.changeExistenceOfAdditionalIngredient(additionalIngredient);
      }}
    >
      <CardActionArea>
        <CardMedia image={imageLink} className={media} title="Photo of ingredient" />
        <CardContent>
          <div className={ingredientName}>{name}</div>
          <Observer>{() => <div>{`Цена: ${price * pizzaStore.size.priceMultiplier}`}</div>}</Observer>
        </CardContent>
      </CardActionArea>
    </Card>
  );
};
