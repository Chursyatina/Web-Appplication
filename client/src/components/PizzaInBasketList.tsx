import React, { useEffect, useState } from 'react';
import {
  Grid,
  Divider,
  Typography,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  FormControl,
  IconButton,
  Input,
  InputAdornment,
  InputLabel,
  TextField,
} from '@material-ui/core';
import { observer } from 'mobx-react-lite';
import MuiPhoneNumber from 'material-ui-phone-number';
import { VisibilityOff, Visibility } from '@material-ui/icons';

import { pizzaListStyles } from 'src/componentsStyles/pizzaListStyles';
import { userStore } from 'src/store/currentUser';
import { IOrder } from 'src/interfaces/order';

import { PizzaInBasket } from './PizzaInBasket';
import { ItsEmpty } from './IstEmpty';

export const PizzaInBasketList = observer(() => {
  const { root, summary, button } = pizzaListStyles();

  const [order, setOrder] = useState<IOrder>({} as IOrder);

  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfirmation, setPasswordConfirmation] = useState('');

  const [isNameError, setNameError] = useState(false);

  const [showPassword, setShowPassword] = useState(false);

  const [signInOpen, setSignInOpen] = useState(false);
  const [signUpOpen, setSignUpOpen] = useState(false);

  const clickSignIn = () => {
    setName('');
    setPhone('');
    setPassword('');
    setPasswordConfirmation('');
    setSignInOpen(!signInOpen);
  };

  const clickSignUp = () => {
    setName('');
    setPhone('');
    setPassword('');
    setPasswordConfirmation('');
    setSignUpOpen(!signUpOpen);
  };

  const handleClickShowPassword = () => {
    setShowPassword(!showPassword);
  };

  return (
    <div>
      {userStore.basket.orderLines.length !== 0 ? (
        <div className={root}>
          <Grid container justify="center">
            {userStore.basket.orderLines.map(orderLine => (
              <Grid item key={orderLine.id}>
                <PizzaInBasket orderLine={orderLine} />
              </Grid>
            ))}
          </Grid>
          <Divider variant="middle" color="primary" />
          <Grid container justify="flex-end" className={summary}>
            <Grid item>
              <Typography variant="h5" component="h5">
                {`Итого: `} {userStore.basket.orderLines.length} {` позиции`}
              </Typography>
              <Typography variant="h5" component="h5">
                {`Сумма заказа: `} {Number(userStore.basket.price).toFixed(3)} {` ₽`}
              </Typography>
              <Button
                className={button}
                onClick={() => {
                  if (userStore.role === 'user') {
                    userStore.createOrder();
                    userStore.clearBasket();
                  } else {
                    clickSignIn();
                  }
                }}
                variant="contained"
                color="primary"
                fullWidth
              >{`Оформить заказ`}</Button>
            </Grid>
          </Grid>
          <div>
            <Dialog open={signInOpen} onClose={setSignInOpen} aria-labelledby="form-dialog-title">
              <DialogTitle id="form-dialog-title">Для совершения заказа требуется авторизоваться</DialogTitle>
              <DialogContent>
                <Grid container justify="center" alignItems="center">
                  <Grid item xs={12}>
                    <MuiPhoneNumber defaultCountry={'ru'} onChange={e => setPhone(e.toString())} />
                  </Grid>
                  <Grid item xs={12}>
                    <FormControl variant="standard">
                      <InputLabel htmlFor="standard-adornment-password">Пароль</InputLabel>
                      <Input
                        id="standard-adornment-password"
                        type={showPassword ? 'text' : 'password'}
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        endAdornment={
                          <InputAdornment position="end">
                            <IconButton aria-label="toggle password visibility" onClick={handleClickShowPassword}>
                              {showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                          </InputAdornment>
                        }
                      />
                    </FormControl>
                  </Grid>
                </Grid>
              </DialogContent>
              <DialogActions>
                <Button onClick={clickSignIn} color="primary">
                  Отмена
                </Button>
                <Button
                  onClick={e => {
                    userStore.signIn(phone, password);
                    clickSignIn();
                  }}
                  color="primary"
                >
                  Войти
                </Button>
                <Button
                  onClick={e => {
                    clickSignIn();
                    clickSignUp();
                  }}
                  color="primary"
                >
                  Регистрация
                </Button>
              </DialogActions>
            </Dialog>
          </div>
          <div>
            <Dialog open={signUpOpen} onClose={setSignUpOpen} aria-labelledby="form-dialog-title">
              <DialogTitle id="form-dialog-title">Регистрация</DialogTitle>
              <DialogContent>
                <Grid container justify="center" alignItems="center">
                  <Grid item xs={12}>
                    {!isNameError ? (
                      <TextField
                        id="standard-basic"
                        label="Имя"
                        variant="standard"
                        onChange={e => {
                          setName(e.target.value);
                        }}
                      />
                    ) : (
                      <TextField
                        error
                        id="standard-basic"
                        label="Имя"
                        variant="standard"
                        helperText="Строка от 1 до 100 символов"
                        onChange={e => {
                          setName(e.target.value);
                        }}
                      />
                    )}
                  </Grid>
                  <Grid item xs={12}>
                    <MuiPhoneNumber defaultCountry={'ru'} onChange={e => setPhone(e.toString())} />
                  </Grid>
                  <Grid item xs={12}>
                    <FormControl variant="standard">
                      <InputLabel htmlFor="standard-adornment-password">Пароль</InputLabel>
                      <Input
                        id="standard-adornment-password"
                        type={showPassword ? 'text' : 'password'}
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        endAdornment={
                          <InputAdornment position="end">
                            <IconButton aria-label="toggle password visibility" onClick={handleClickShowPassword}>
                              {showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                          </InputAdornment>
                        }
                      />
                    </FormControl>
                  </Grid>
                  <Grid item xs={12}>
                    <FormControl variant="standard">
                      <InputLabel htmlFor="standard-adornment-password">Подтверждение пароля</InputLabel>
                      <Input
                        id="standard-adornment-password"
                        type={showPassword ? 'text' : 'password'}
                        value={passwordConfirmation}
                        onChange={e => setPasswordConfirmation(e.target.value)}
                        endAdornment={
                          <InputAdornment position="end">
                            <IconButton aria-label="toggle password visibility" onClick={handleClickShowPassword}>
                              {showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                          </InputAdornment>
                        }
                      />
                    </FormControl>
                  </Grid>
                </Grid>
              </DialogContent>
              <DialogActions>
                <Button onClick={clickSignUp} color="primary">
                  Отмена
                </Button>
                <Button
                  onClick={e => {
                    if (name.length < 1 || name.length > 100) {
                      setNameError(true);
                    } else {
                      userStore.signUp(name, phone, password, passwordConfirmation);
                      clickSignUp();
                    }
                  }}
                  color="primary"
                >
                  Зарегистрироваться
                </Button>
              </DialogActions>
            </Dialog>
          </div>
        </div>
      ) : (
        <ItsEmpty />
      )}
    </div>
  );
});
