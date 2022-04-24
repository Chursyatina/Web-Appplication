import React from 'react';
import { Toolbar, IconButton, Typography, InputBase, Badge } from '@material-ui/core';
import { Menu, Search, ShoppingBasket, Edit } from '@material-ui/icons';
import { useHistory } from 'react-router';
import { observer } from 'mobx-react-lite';

import { primaryAppBarStyles } from 'src/componentsStyles/primaryAppBarStyles';
import { userStore } from 'src/store/currentUser';

export const PrimarySearchAppBarToolBar = observer(() => {
  const { menuButton, title, search, searchIcon, inputRoot, inputInput } = primaryAppBarStyles();

  const history = useHistory();

  return (
    <Toolbar>
      <IconButton edge="start" className={menuButton} color="inherit" aria-label="open drawer">
        <Menu />
      </IconButton>
      <Typography className={title} variant="h6" noWrap>
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
        </div>
      ) : (
        <div>
          <IconButton
            aria-label="account of current user"
            aria-controls="menu-appbar"
            aria-haspopup="true"
            color="inherit"
            onClick={() => {
              history.push('/Catalogs');
            }}
          >
            <Edit />
          </IconButton>
        </div>
      )}
    </Toolbar>
  );
});
