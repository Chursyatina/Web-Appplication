import React, { useState } from 'react';
import { Grid, Typography, Button, Modal, TextField, Card, CardActionArea, CardMedia } from '@material-ui/core';
import { observer, Observer } from 'mobx-react-lite';

import { IPizzaProps } from 'src/interfaces/pizza';
import { SizeTabs } from 'src/components/SizeTabs';
import { DoughTabs } from 'src/components/DoughTabs';
import { AdditionalIngredientsList } from 'src/components/AdditionalIngredientsList';
import { pizzaDialogStyles } from 'src/componentsStyles/pizzaDialogStyles';
import { pizzaStore } from 'src/store/currentPizza';
import { IngredientsListForPizzaCreating } from 'src/components/IngredientsListForPizzaCreating';
import { creatingPizzaStore } from 'src/store/creatingPizza';

export const AddingNewPizzaDialog = observer(() => {
  const { root, media, button, center, paper, pizzaImage, basketItem, buyButton, tab } = pizzaDialogStyles();

  const [open, setOpen] = useState(false);
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');

  const clickHandler = () => {
    setOpen(!open);
    creatingPizzaStore.removePizza();
  };

  const body = (
    <div className={paper}>
      <Grid container>
        <Grid item xs={7} className={pizzaImage}>
          <img
            src="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pngwing.com%2Fru%2Ffree-png-zbdea&psig=AOvVaw35swaS506XxLQrU6a4JiJT&ust=1651695889406000&source=images&cd=vfe&ved=0CAwQjRxqFwoTCNC8oOiUxPcCFQAAAAAdAAAAABAD"
            width="539"
            height="500"
          />
        </Grid>
        <Grid item xs={5} className={basketItem}>
          <TextField
            fullWidth
            id="standard-basic"
            label="Название"
            variant="standard"
            onChange={e => setName(e.target.value)}
          >
            {creatingPizzaStore.name}
          </TextField>
          <TextField
            fullWidth
            id="standard-multiline-static"
            multiline
            rows={4}
            label="Описание"
            variant="standard"
            onChange={e => setDescription(e.target.value)}
          >
            {creatingPizzaStore.description}
          </TextField>
          <div>
            <Typography variant="h6">Ингредиенты</Typography>
            <IngredientsListForPizzaCreating />
          </div>
        </Grid>
        <Grid item xs={12}>
          <Grid container justify="flex-end">
            <Observer>
              {() => (
                <Button
                  variant="contained"
                  color="primary"
                  className={buyButton}
                  onClick={() => {
                    creatingPizzaStore.setName(name);
                    creatingPizzaStore.setDescription(description);
                    creatingPizzaStore.createNewPizza();
                    setOpen(!open);
                  }}
                >
                  {`Создать пиццу, начальная цена = ${creatingPizzaStore.price} $`}
                </Button>
              )}
            </Observer>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );

  return (
    <div>
      <div onClick={clickHandler}>
        <Card className={root}>
          <CardActionArea>
            <CardMedia
              image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pngwing.com%2Fru%2Ffree-png-zbdea&psig=AOvVaw35swaS506XxLQrU6a4JiJT&ust=1651695889406000&source=images&cd=vfe&ved=0CAwQjRxqFwoTCNC8oOiUxPcCFQAAAAAdAAAAABAD"
              title="Photo of pizza"
              className={media}
            />
          </CardActionArea>
        </Card>
      </div>
      <Modal
        open={open}
        onClose={clickHandler}
        aria-labelledby="simple-modal-title"
        aria-describedby="simple-modal-description"
      >
        {body}
      </Modal>
    </div>
  );
});
