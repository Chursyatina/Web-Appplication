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
    fieldwidth: {
      width: 150,
      marginLeft: 85,
      textAlign: 'center',
    },
    button: {
      margin: 20,
    },
    loadLine1: {
      width: '100%',
      '& > * + *': {
        marginTop: 400,
      },
      marginTop: 350,
    },
  }),
);
