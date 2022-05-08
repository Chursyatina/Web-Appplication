import {
  Button,
  Checkbox,
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
import { IAdditionalIngredientProps } from 'src/interfaces/additionalIngredient';
import { IIngredientProps } from 'src/interfaces/ingredient';
import { menuStore } from 'src/store/currentMenu';
import { userStore } from 'src/store/currentUser';

import { PizzaList } from './PizzaList';

export const NameForEditting = (props: IAdditionalIngredientProps) => {
  const { root, button, center } = catalogsEdittingButtonsStyles();

  return (
    <div>
      <h4 className={center}>{props.additionalIngredient.name}</h4>
    </div>
  );
};

export const PriceForEditting = (props: IAdditionalIngredientProps) => {
  const { root, button, center } = catalogsEdittingButtonsStyles();

  return (
    <div>
      <h4 className={center}>{`${props.additionalIngredient.price} р.`}</h4>
    </div>
  );
};

export const ButtonForEditting = (props: IAdditionalIngredientProps) => {
  const { root, button, center, editButton, iconRoot, divCenter } = catalogsEdittingButtonsStyles();

  const [open, setOpen] = React.useState(false);
  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [image, setImage] = useState('');
  const [isDeleted, setDeleted] = useState(false);
  const [isAvailable, setAvalabness] = useState(true);
  const [id, setId] = useState('');

  const getCoverBase64 = (file: Blob) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      console.log(typeof reader.result === 'string' ? reader.result : '');
      setImage(typeof reader.result === 'string' ? reader.result : '');
    };
  };

  const handleClickOpen = () => {
    setOpen(true);
    setName(props.additionalIngredient.name);
    setPrice(props.additionalIngredient.price);
    setImage(props.additionalIngredient.imageLink);
    setDeleted(props.additionalIngredient.isDeleted);
    setAvalabness(props.additionalIngredient.isAvailable);
    setId(props.additionalIngredient.id);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const updateIng = async () => {
    menuStore.updateAdditionalIngredient(id, name, image, price, isAvailable);
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
        <DialogTitle id="form-dialog-title">Изменение добавки</DialogTitle>
        <DialogContent>
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
          <TextField
            margin="dense"
            id="price"
            label="Цена"
            type="email"
            fullWidth
            value={price}
            onChange={e => setPrice(Number(e.target.value))}
          />
          Наличие
          <Checkbox
            checked={isAvailable}
            onChange={e => {
              setAvalabness(!isAvailable);
            }}
            inputProps={{ 'aria-label': 'controlled' }}
          />
          <div
            onClick={() => {
              document.getElementById('image')?.click();
            }}
            className={divCenter}
          >
            <img
              src={
                image !== ''
                  ? image
                  : 'https://img2.freepng.ru/20180315/qae/kisspng-computer-icons-plus-sign-clip-art-plus-sign-5aaad899307aa1.3479178215211460091986.jpg'
              }
              className={iconRoot}
            />
            <input
              type="file"
              accept="image/png, image/jpeg"
              style={{ display: 'none' }}
              id="image"
              onChange={async e => {
                const { files } = e.target;
                if (files && files.length > 0) {
                  const file = files[0];
                  getCoverBase64(file);
                }
              }}
            />
          </div>
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

export const AvialabnessForEditting = (props: IAdditionalIngredientProps) => {
  const { root, button, center, editButton, iconRoot, divCenter } = catalogsEdittingButtonsStyles();

  const changeAvialabnessOfIngredient = async () => {
    menuStore.updateAdditionalIngredient(
      props.additionalIngredient.id,
      props.additionalIngredient.name,
      props.additionalIngredient.imageLink,
      props.additionalIngredient.price,
      !props.additionalIngredient.isAvailable,
    );
  };

  if (props.additionalIngredient.isAvailable === true) {
    return (
      <div className={center}>
        <Button
          onClick={() => {
            changeAvialabnessOfIngredient();
          }}
          color="primary"
          className={button}
        >
          {' '}
          Есть в наличии{' '}
        </Button>
      </div>
    );
  } else {
    return (
      <div className={center}>
        <Button
          onClick={() => {
            changeAvialabnessOfIngredient();
          }}
          color="primary"
          className={button}
        >
          {' '}
          Нет в наличии{' '}
        </Button>
      </div>
    );
  }
};

export const DelButtonForEditting = (props: IAdditionalIngredientProps) => {
  const { root, button, center, iconcenter } = catalogsEdittingButtonsStyles();

  const deleteIng = async () => {
    menuStore.removeAdditionalIngredient(props.additionalIngredient.id);
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
