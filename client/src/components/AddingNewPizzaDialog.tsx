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

  const [bigCover, setBigCover] = useState('');
  const [cover, setCover] = useState('');

  const getBigCoverBase64 = (file: Blob) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      setBigCover(typeof reader.result === 'string' ? reader.result : '');
    };
  };

  const getCoverBase64 = (file: Blob) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      setCover(typeof reader.result === 'string' ? reader.result : '');
    };
  };

  const clickHandler = () => {
    setOpen(!open);
    creatingPizzaStore.removePizza();
    setCover('');
    setBigCover('');
  };

  const body = (
    <div className={paper}>
      <Grid container>
        <Grid item xs={7} className={pizzaImage}>
          <div
            onClick={() => {
              document.getElementById('bigfileinput')?.click();
            }}
          >
            <img
              src={bigCover !== '' ? bigCover : 'http://artismedia.by/blog/wp-content/uploads/2018/05/in-blog2-1.png'}
              width="539"
              height="500"
            />
            <input
              type="file"
              accept="image/png, image/jpeg"
              style={{ display: 'none' }}
              id="bigfileinput"
              onChange={async e => {
                const { files } = e.target;
                if (files && files.length > 0) {
                  const file = files[0];
                  getBigCoverBase64(file);
                }
              }}
            />
          </div>
        </Grid>
        <Grid item xs={5} className={basketItem}>
          <div
            onClick={() => {
              document.getElementById('fileinput')?.click();
            }}
          >
            <img
              src={cover !== '' ? cover : 'http://artismedia.by/blog/wp-content/uploads/2018/05/in-blog2-1.png'}
              width="360"
              height="200"
            />
            <input
              type="file"
              accept="image/png, image/jpeg"
              style={{ display: 'none' }}
              id="fileinput"
              onChange={async e => {
                const { files } = e.target;
                if (files && files.length > 0) {
                  const file = files[0];
                  getCoverBase64(file);
                }
              }}
            />
          </div>
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
                    creatingPizzaStore.setImageLink(cover);
                    creatingPizzaStore.setSingleImageLink(bigCover);
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
              image="http://artismedia.by/blog/wp-content/uploads/2018/05/in-blog2-1.png"
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
