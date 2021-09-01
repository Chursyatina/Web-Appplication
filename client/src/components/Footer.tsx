import { IconButton, Grid } from '@material-ui/core';
import React from 'react';
import { Twitter, Facebook, Mail } from '@material-ui/icons';

import { footerStyles } from 'src/componentsStyles/footerStyles';

export const Footer = () => {
  const { root, content } = footerStyles();

  return (
    <Grid container alignContent="center" className={root}>
      <Grid item className={content}>
        <IconButton>
          <Twitter />
        </IconButton>
        <IconButton>
          <Facebook />
        </IconButton>
        <IconButton>
          <Mail />
        </IconButton>
        <div>2021-2021 Компания Yo-Yo Pizza.</div>
      </Grid>
    </Grid>
  );
};
