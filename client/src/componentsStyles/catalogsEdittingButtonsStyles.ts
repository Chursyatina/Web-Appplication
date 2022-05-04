import { createStyles, makeStyles } from '@material-ui/core/styles';
import { Height } from '@material-ui/icons';

export const catalogsEdittingButtonsStyles = makeStyles(theme =>
  createStyles({
    root: {
      maxWidth: 370,
      margin: 15,
    },
    button: {
      margin: 2,
    },
    editButton: {
      '& .MuiIconButton-root': {
        padding: 0,
      },
      '& .MuiButton-text': {
        padding: 0,
      },
    },
    center: {
      marginTop: 70,
      marginBottom: 110,
      textAlign: 'center',
      '& .MuiIconButton-root': {
        padding: 0,
      },
      '& .MuiButton-text': {
        padding: 0,
      },
      '& .MuiButton-root': {
        padding: 0,
        height: 1,
      },
    },
    iconcenter: {
      '& .MuiIconButton-root': {
        padding: 0,
      },
      textAlign: 'center',
      marginTop: 65,
      marginBottom: 103,
    },
    iconRoot: {
      width: 108,
      height: 120,
      margin: 4,
      transition: 'all 0.3s ease',
      borderRadius: '10px',
      borderColor: 'black',
      boxSizing: 'border-box',
    },
    divCenter: {
      marginTop: 20,
      alignItems: 'center',
      textAlign: 'center',
    },
  }),
);
