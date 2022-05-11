import React, { useState } from 'react';
import { Grid, Typography, Button, Modal, TextField, Card, CardActionArea, CardMedia } from '@material-ui/core';
import { observer, Observer } from 'mobx-react-lite';

import { pizzaStore } from 'src/store/currentPizza';
import { IngredientsListForPizzaCreating } from 'src/components/IngredientsListForPizzaCreating';
import { creatingPizzaStore } from 'src/store/creatingPizza';
import { menuStore } from 'src/store/currentMenu';
import { pizzaDialogStyles } from 'src/componentsStyles/pizzaDialogStyles';
import { IPizzaProps } from 'src/interfaces/pizza';

import { IngredientsListForEditting } from './IngredinetListForEditting';

export const EdittingPizzaDialog = observer((props: IPizzaProps) => {
  const { root, media, button, center, paper, pizzaImage, basketItem, buyButton, tab } = pizzaDialogStyles();

  const { pizza } = props;
  const { id, ingredients, singleItemImageLink, imageLink, name, description, price } = pizza;

  const [open, setOpen] = useState(false);
  const [currentName, setName] = useState('');
  const [currentDescription, setDescription] = useState('');

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
    creatingPizzaStore.removePizza();
    setName(name);
    creatingPizzaStore.setName(name);
    setDescription(description);
    creatingPizzaStore.setDescription(description);
    setCover(imageLink);
    creatingPizzaStore.setImageLink(imageLink);
    setBigCover(singleItemImageLink);
    creatingPizzaStore.setSingleImageLink(singleItemImageLink);
    ingredients.forEach(element => {
      if (!element.isDeleted) {
        creatingPizzaStore.changeExistenceOfIngredient(element);
        console.log('added ingredient');
      }
    });
    setOpen(!open);
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
            defaultValue={currentName}
            onChange={e => setName(e.target.value)}
            inputProps={{ minLength: 1, maxLength: 20 }}
          >
            {currentName}
          </TextField>
          <TextField
            fullWidth
            id="standard-multiline-static"
            multiline
            rows={4}
            label="Описание"
            variant="standard"
            defaultValue={currentDescription}
            onChange={e => setDescription(e.target.value)}
            inputProps={{ minLength: 10, maxLength: 150 }}
          >
            {currentDescription}
          </TextField>
          <div>
            <Typography variant="h6">Ингредиенты</Typography>
            <IngredientsListForEditting pizza={pizza} />
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
                    creatingPizzaStore.setId(id);
                    creatingPizzaStore.setName(name);
                    creatingPizzaStore.setDescription(description);
                    creatingPizzaStore.setImageLink(cover);
                    creatingPizzaStore.setSingleImageLink(bigCover);
                    menuStore.updatePizza(id, creatingPizzaStore.getEdittingPizza());
                    setOpen(!open);
                  }}
                >
                  {`Cохранить пиццу, начальная цена = ${creatingPizzaStore.price} $`}
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
      <div className={center}>
        <Button className={button} onClick={clickHandler}>
          Edit
        </Button>
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
