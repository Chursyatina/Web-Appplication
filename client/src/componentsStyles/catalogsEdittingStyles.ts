import { createStyles, makeStyles } from '@material-ui/core/styles';

export const catalogsEdittingStyles = makeStyles(() =>
  createStyles({
    root: {
      flexGrow: 1,
      textAlign: 'center',
    },

    addButton: {
      position: 'fixed',
      right: '50px',
      bottom: '125px',
    },
    center: {
      textAlign: 'center',
    },
    namefieldwidth: {
      width: 150,
      marginLeft: 40,
      textAlign: 'center',
    },
    pricefieldwidth: {
      width: 150,
      marginLeft: 30,
      textAlign: 'center',
    },
    button: {
      '& .makeStyles-button-37': {
        margin: 0,
      },
      marginLeft: 20,
      marginBottom: 100,
    },
    loadLine1: {
      width: '100%',
      '& > * + *': {
        marginTop: 400,
      },
      marginTop: 350,
    },
    iconRoot: {
      width: 108,
      height: 120,
      margin: 4,
      transition: 'all 0.3s ease',
      borderRadius: '10px',
      boxSizing: 'border-box',
    },
    media: {
      height: 120,
    },
    border: {
      border: '1px solid darkblue',
    },
  }),
);
