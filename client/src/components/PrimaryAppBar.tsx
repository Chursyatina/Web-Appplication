import React from 'react';
import AppBar from '@material-ui/core/AppBar';

import { primaryAppBarStyles } from 'src/componentsStyles/primaryAppBarStyles';

import { PrimarySearchAppBarToolBar } from './PrimarySearchAppBarToobar';

export const PrimaryAppBar = () => {
  const { root } = primaryAppBarStyles();

  return (
    <div className={root}>
      <AppBar position="static">
        <PrimarySearchAppBarToolBar />
      </AppBar>
    </div>
  );
};
