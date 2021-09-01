import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

export const pizzaDialogStyles = makeStyles((theme: Theme) =>
  createStyles({
    '@global': {
      '*::-webkit-scrollbar': {
        width: '0.4em',
      },
      '*::-webkit-scrollbar-track': {
        '-webkit-box-shadow': 'inset 0 0 6px rgba(0,0,0,0.00)',
      },
      '*::-webkit-scrollbar-thumb': {
        backgroundColor: 'rgba(57, 57, 57, 0.3)',
        borderRadius: 20,
      },
    },
    button: {
      margin: 2,
      fontFamily: 'Segoe UI',
    },
    center: {
      textAlign: 'center',
    },
    paper: {
      top: `50%`,
      left: `50%`,
      position: 'relative',
      height: 610,
      width: 924,
      marginTop: -610 / 2,
      marginLeft: -924 / 2,
      backgroundColor: theme.palette.background.paper,
      borderRadius: 20,
      boxShadow: theme.shadows[5],
    },
    pizzaImage: {
      marginTop: 50,
    },
    basketItem: {
      backgroundColor: '#fcfcfc',
      marginTop: 20,
      padding: 12,
      overflowY: 'scroll',
      height: 530,
    },
    withNoMarginsAndPaddings: {
      padding: 0,
      margin: 0,
    },
    buyButton: {
      marginRight: 50,
      marginTop: 7,
    },
    tab: {
      marginBottom: 15,
    },
  }),
);
