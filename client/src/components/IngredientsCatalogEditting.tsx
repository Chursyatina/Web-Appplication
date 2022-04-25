import {
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  Grid,
  IconButton,
  LinearProgress,
  TextField,
} from '@material-ui/core';
import { Delete } from '@material-ui/icons';
import React, { useEffect, useState } from 'react';

import { catalogsEdittingButtonsStyles } from 'src/componentsStyles/catalogsEdittingButtonsStyles';
import { catalogsEdittingStyles } from 'src/componentsStyles/catalogsEdittingStyles';
import { IIngredientProps } from 'src/interfaces/ingredient';
import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';

import { PizzaList } from './PizzaList';

export const NameForEditting = (props: IIngredientProps) => {
  const { root, button, center } = catalogsEdittingButtonsStyles();

  return (
    <div>
      <h4 className={center}>{props.ingredient.name}</h4>
    </div>
  );
};

export const PriceForEditting = (props: IIngredientProps) => {
  const { root, button, center } = catalogsEdittingButtonsStyles();

  return (
    <div>
      <h4 className={center}>{`${props.ingredient.price} р.`}</h4>
    </div>
  );
};

export const ButtonForEditting = (props: IIngredientProps) => {
  const { root, button, center } = catalogsEdittingButtonsStyles();

  const [open, setOpen] = React.useState(false);
  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [image, setImage] = useState('');
  const [isDeleted, setDeleted] = useState(false);
  const [id, setId] = useState('');

  const handleClickOpen = () => {
    setOpen(true);
    setName(props.ingredient.name);
    setPrice(props.ingredient.price);
    setImage(props.ingredient.imageLink);
    setDeleted(props.ingredient.isDeleted);
    setId(props.ingredient.id);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const updateIng = async () => {
    menuStore.updateIngredient(id, name, image, price);
  };

  return (
    <div>
      <div className={center}>
        <Button color="primary" className={button} onClick={handleClickOpen}>
          {' '}
          Редактировать
        </Button>
      </div>

      <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title">Изменение ингредиента</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="ingname"
            label="Название ингредиента"
            type="email"
            fullWidth
            value={name}
            onChange={e => setName(e.target.value)}
          />
          <TextField
            margin="dense"
            id="price"
            label="Цена ингредиента"
            type="email"
            fullWidth
            value={price}
            onChange={e => setPrice(Number(e.target.value))}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Отмена
          </Button>
          <Button
            onClick={e => {
              handleClose();
              updateIng();
            }}
            color="primary"
          >
            Сохранить
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export const DelButtonForEditting = (props: IIngredientProps) => {
  const { root, button, center, iconcenter } = catalogsEdittingButtonsStyles();

  const deleteIng = async () => {
    menuStore.removeIngredient(props.ingredient.id);
  };

  return (
    <div className={iconcenter}>
      <IconButton
        onClick={() => {
          deleteIng();
        }}
        color="primary"
        className={button}
      >
        <Delete />
      </IconButton>
    </div>
  );
};
