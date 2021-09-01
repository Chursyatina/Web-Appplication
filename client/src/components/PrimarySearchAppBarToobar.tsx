import React from 'react';
import { Toolbar, IconButton, Typography, InputBase } from '@material-ui/core';
import { Menu, Search } from '@material-ui/icons';

import { primaryAppBarStyles } from 'src/componentsStyles/primaryAppBarStyles';

export const PrimarySearchAppBarToolBar = () => {
  const { menuButton, title, search, searchIcon, inputRoot, inputInput } = primaryAppBarStyles();

  return (
    <Toolbar>
      <IconButton edge="start" className={menuButton} color="inherit" aria-label="open drawer">
        <Menu />
      </IconButton>
      <Typography className={title} variant="h6" noWrap>
        YoYo Pizza
      </Typography>
      <div className={search}>
        <div className={searchIcon}>
          <Search />
        </div>
        <InputBase
          placeholder="Search…"
          classes={{
            root: inputRoot,
            input: inputInput,
          }}
          inputProps={{ 'aria-label': 'search' }}
        />
      </div>
    </Toolbar>
  );
};
