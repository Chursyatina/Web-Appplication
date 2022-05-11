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
import { menuStore } from 'src/store/currentMenu';

export const AddingNewPizzaDialog = observer(() => {
  const { root, media, button, center, paper, pizzaImage, basketItem, buyButton, tab } = pizzaDialogStyles();

  const [open, setOpen] = useState(false);
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [discount, setDiscount] = useState(0);
  const [bonusCoef, setBonusCoef] = useState(0);

  const [isNameError, setNameError] = useState(false);
  const [isDescriptionError, setDescriptionError] = useState(false);

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
              src={
                bigCover !== ''
                  ? bigCover
                  : 'https://img2.freepng.ru/20180315/qae/kisspng-computer-icons-plus-sign-clip-art-plus-sign-5aaad899307aa1.3479178215211460091986.jpg'
              }
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
              src={
                cover !== ''
                  ? cover
                  : 'https://img2.freepng.ru/20180315/qae/kisspng-computer-icons-plus-sign-clip-art-plus-sign-5aaad899307aa1.3479178215211460091986.jpg'
              }
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
          {!isNameError ? (
            <TextField
              fullWidth
              id="standard-basic"
              label="Название"
              variant="standard"
              onChange={e => setName(e.target.value)}
            >
              {creatingPizzaStore.name}
            </TextField>
          ) : (
            <TextField
              error
              fullWidth
              id="F"
              label="Название"
              variant="standard"
              helperText="Строка от 1 до 20 символов"
              onChange={e => setName(e.target.value)}
            >
              {creatingPizzaStore.name}
            </TextField>
          )}
          {!isDescriptionError ? (
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
          ) : (
            <TextField
              error
              fullWidth
              id="standard-basic"
              multiline
              rows={4}
              label="Описание"
              variant="standard"
              helperText="Строка от 10 до 150 символов"
              onChange={e => setDescription(e.target.value)}
            >
              {creatingPizzaStore.description}
            </TextField>
          )}
          <TextField
            fullWidth
            id="standard-basic"
            multiline
            label="Скидка"
            variant="standard"
            onChange={e => setDiscount(Number(e.target.value))}
          >
            {creatingPizzaStore.discount}
          </TextField>

          <TextField
            fullWidth
            id="standard-multiline-static"
            multiline
            label="Бонусный коэффициент"
            variant="standard"
            onChange={e => setBonusCoef(Number(e.target.value))}
          >
            {creatingPizzaStore.bonusCoef}
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
                    creatingPizzaStore.setBonusCoef(bonusCoef);
                    creatingPizzaStore.setDiscount(discount);

                    setNameError(false);
                    setDescriptionError(false);
                    if (name.length < 1 || name.length > 20) {
                      setNameError(true);
                    } else if (description.length < 10 || description.length > 150) {
                      setDescriptionError(true);
                    } else {
                      menuStore.createPizza(creatingPizzaStore.getNewPizza());
                      setOpen(!open);
                    }
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
              image="https://img2.freepng.ru/20180315/qae/kisspng-computer-icons-plus-sign-clip-art-plus-sign-5aaad899307aa1.3479178215211460091986.jpg"
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
