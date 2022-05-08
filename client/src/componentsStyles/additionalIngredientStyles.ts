import { createStyles, makeStyles } from '@material-ui/core/styles';

export const additionalIngredientStyles = makeStyles(() =>
  createStyles({
    root: {
      width: 108,
      height: 200,
      margin: 4,
      position: 'relative',
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
    ingredientName: {
      height: 32,
    },
    divBack: {
      backgroundColor: 'grey',
      position: 'absolute',
      left: 0,
      right: 0,
      bottom: 0,
      top: 0,
      opacity: 0.8,
      height: 120,
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'center',
      alignContent: 'center',
    },
    notAvialable: {
      alignContent: 'center',
      alignSelf: 'center',
      verticalAlign: 'middle',
    },
  }),
);
