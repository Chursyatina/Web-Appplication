import React, { useState } from 'react';
import {
  Toolbar,
  IconButton,
  Typography,
  InputBase,
  Badge,
  MenuItem,
  Menu,
  Modal,
  Dialog,
  DialogTitle,
  DialogContent,
  FormControl,
  InputLabel,
  InputAdornment,
  Input,
  Button,
  DialogActions,
  Grid,
  TextField,
} from '@material-ui/core';
import { Search, ShoppingBasket, Edit, AccountCircle, VisibilityOff, Visibility } from '@material-ui/icons';
import { useHistory } from 'react-router';
import { observer } from 'mobx-react-lite';
import MuiPhoneNumber from 'material-ui-phone-number';

import { primaryAppBarStyles } from 'src/componentsStyles/primaryAppBarStyles';
import { userStore } from 'src/store/currentUser';

export const PrimarySearchAppBarToolBar = observer(() => {
  const { userName, menuButton, title, search, searchIcon, inputRoot, inputInput } = primaryAppBarStyles();

  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);

  const history = useHistory();

  const [name, setName] = useState('');
  const [phone, setPhone] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfirmation, setPasswordConfirmation] = useState('');

  const [showPassword, setShowPassword] = useState(false);

  const [signInOpen, setSignInOpen] = useState(false);
  const [signUpOpen, setSignUpOpen] = useState(false);

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

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
      <Toolbar>
        <IconButton edge="start" className={menuButton} color="inherit" aria-label="open drawer" />
        <Typography className={title} variant="h4" noWrap>
          <IconButton
            edge="start"
            color="inherit"
            aria-label="open drawer"
            onClick={() => {
              history.push('/');
            }}
          >
            YoYo Pizza
          </IconButton>
        </Typography>
        {userStore.role !== 'admin' ? (
          <div>
            <Grid container>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  color="inherit"
                  onClick={() => {
                    history.push('/Basket');
                  }}
                >
                  <Badge badgeContent={userStore.basket.orderLines.length} color="secondary">
                    <ShoppingBasket />
                  </Badge>
                </IconButton>
              </Grid>
              <Grid item justify="center">
                <Typography variant="h5" className={userName}>
                  {userStore.name}
                </Typography>
              </Grid>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  onClick={handleMenu}
                  color="inherit"
                >
                  <AccountCircle />
                </IconButton>
              </Grid>
            </Grid>
            {userStore.role !== 'user' ? (
              <div>
                <Menu
                  id="menu-appbar"
                  anchorEl={anchorEl}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={Boolean(anchorEl)}
                  onClose={handleClose}
                >
                  <MenuItem
                    onClick={e => {
                      setSignInOpen(!signInOpen);
                    }}
                  >
                    Вход
                  </MenuItem>
                  <MenuItem
                    onClick={e => {
                      handleClose();
                      setSignUpOpen(!signUpOpen);
                    }}
                  >
                    Регистрация
                  </MenuItem>
                </Menu>
              </div>
            ) : (
              <div>
                <Menu
                  id="menu-appbar"
                  anchorEl={anchorEl}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={Boolean(anchorEl)}
                  onClose={handleClose}
                >
                  <MenuItem onClick={handleClose}>История заказов</MenuItem>
                  <MenuItem
                    onClick={e => {
                      handleClose();
                      userStore.signOut();
                    }}
                  >
                    Выйти
                  </MenuItem>
                </Menu>
              </div>
            )}
          </div>
        ) : (
          <div>
            <Grid container>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  color="inherit"
                  onClick={() => {
                    history.push('/DoughsCatalog');
                  }}
                >
                  <Edit />
                </IconButton>
              </Grid>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  color="inherit"
                  onClick={() => {
                    history.push('/SizesCatalog');
                  }}
                >
                  <Edit />
                </IconButton>
              </Grid>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  color="inherit"
                  onClick={() => {
                    history.push('/AdditionalIngredientsCatalog');
                  }}
                >
                  <Edit />
                </IconButton>
              </Grid>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  color="inherit"
                  onClick={() => {
                    history.push('/IngredientsCatalog');
                  }}
                >
                  <Edit />
                </IconButton>
              </Grid>
              <Grid item>
                <Typography className={userName} variant="h5">
                  {userStore.name}
                </Typography>
              </Grid>
              <Grid item>
                <IconButton
                  aria-label="account of current user"
                  aria-controls="menu-appbar"
                  aria-haspopup="true"
                  onClick={handleMenu}
                  color="inherit"
                >
                  <AccountCircle />
                </IconButton>
              </Grid>
            </Grid>
            <Menu
              id="menu-appbar"
              anchorEl={anchorEl}
              anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              open={Boolean(anchorEl)}
              onClose={handleClose}
            >
              <MenuItem
                onClick={() => {
                  history.push('/OrdersHistory');
                }}
              >
                Истории заказов
              </MenuItem>
              <MenuItem
                onClick={e => {
                  handleClose();
                  userStore.signOut();
                }}
              >
                Выйти
              </MenuItem>
            </Menu>
          </div>
        )}
      </Toolbar>
      <div>
        <Dialog open={signInOpen} onClose={setSignInOpen} aria-labelledby="form-dialog-title">
          <DialogTitle id="form-dialog-title">Вход</DialogTitle>
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
                handleClose();
                userStore.signIn(phone, password);
                clickSignIn();
              }}
              color="primary"
            >
              Войти
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
                <TextField
                  id="standard-basic"
                  label="Имя"
                  variant="standard"
                  onChange={e => {
                    setName(e.target.value);
                  }}
                />
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
                handleClose();
                userStore.signUp(name, phone, password, passwordConfirmation);
                clickSignUp();
              }}
              color="primary"
            >
              Зарегистрироваться
            </Button>
          </DialogActions>
        </Dialog>
      </div>
    </div>
  );
});
