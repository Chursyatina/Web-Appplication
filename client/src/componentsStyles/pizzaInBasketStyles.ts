import { createStyles, makeStyles } from '@material-ui/core/styles';

export const pizzaInBasketStyles = makeStyles(() =>
  createStyles({
    root: {
      width: '100%',
      height: '100%',
      boxShadow: 'none',
      background: 'transparent',
    },
    cover: {
      width: '100%',
    },
  }),
);
