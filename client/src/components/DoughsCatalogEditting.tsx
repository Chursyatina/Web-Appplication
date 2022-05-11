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
import { sizesDoughsCatalogsEdittingStyles } from 'src/componentsStyles/sizesdougsCatalogsEdittingStyles';
import { IAdditionalIngredientProps } from 'src/interfaces/additionalIngredient';
import { IIngredientProps } from 'src/interfaces/ingredient';
import { IPizzaDoughProps } from 'src/interfaces/pizzaDough';
import { IPizzaSizeProps } from 'src/interfaces/pizzaSize';
import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';

import { PizzaList } from './PizzaList';

export const NameForEditting = (props: IPizzaDoughProps) => {
  const { root, button, center } = sizesDoughsCatalogsEdittingStyles();

  return (
    <div>
      <h4 className={center}>{props.dough.name}</h4>
    </div>
  );
};

export const PriceForEditting = (props: IPizzaDoughProps) => {
  const { root, button, center } = sizesDoughsCatalogsEdittingStyles();

  return (
    <div>
      <h4 className={center}>{`X ${props.dough.priceMultiplier}`}</h4>
    </div>
  );
};

export const ButtonForEditting = (props: IPizzaDoughProps) => {
  const { root, button, center } = sizesDoughsCatalogsEdittingStyles();

  const [open, setOpen] = React.useState(false);
  const [name, setName] = useState('');
  const [priceMultiplier, setPriceMultiplier] = useState(0);
  const [isDeleted, setDeleted] = useState(false);
  const [id, setId] = useState('');

  const [isNameError, setNameError] = useState(false);
  const [isPriceError, setPriceError] = useState(false);

  const handleClickOpen = () => {
    setOpen(true);
    setName(props.dough.name);
    setPriceMultiplier(props.dough.priceMultiplier);
    setDeleted(props.dough.isDeleted);
    setId(props.dough.id);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const updateIng = async () => {
    menuStore.updateDough(id, name, priceMultiplier);
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
        <DialogTitle id="form-dialog-title">Изменение типа теста</DialogTitle>
        <DialogContent>
          {!isNameError ? (
            <TextField
              autoFocus
              margin="dense"
              id="ingname"
              label="Название"
              type="email"
              fullWidth
              value={name}
              onChange={e => setName(e.target.value)}
            />
          ) : (
            <TextField
              error
              autoFocus
              margin="dense"
              id="ingname"
              label="Название"
              type="email"
              fullWidth
              value={name}
              helperText="Строка от 1 до 20 символов"
              onChange={e => setName(e.target.value)}
            />
          )}
          {!isPriceError ? (
            <TextField
              margin="dense"
              id="price"
              label="Цена"
              type="email"
              fullWidth
              value={priceMultiplier}
              onChange={e => setPriceMultiplier(Number(e.target.value))}
            />
          ) : (
            <TextField
              error
              margin="dense"
              id="price"
              label="Цена"
              type="email"
              fullWidth
              helperText="Число от 0.1 до 3"
              value={priceMultiplier}
              onChange={e => setPriceMultiplier(Number(e.target.value))}
            />
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} color="primary">
            Отмена
          </Button>
          <Button
            onClick={e => {
              setNameError(false);
              setPriceError(false);
              if (name.length < 1 || name.length > 20) {
                setNameError(true);
              } else if (
                isNaN(Number(priceMultiplier.toString())) ||
                Number(priceMultiplier.toString()) < 0.1 ||
                Number(priceMultiplier.toString()) > 3
              ) {
                setPriceError(true);
              } else {
                handleClose();
                updateIng();
              }
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

export const DelButtonForEditting = (props: IPizzaDoughProps) => {
  const { root, button, center, iconcenter } = sizesDoughsCatalogsEdittingStyles();

  const deleteIng = async () => {
    menuStore.removeDough(props.dough.id);
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
