import { createStyles, makeStyles } from '@material-ui/core/styles';

export const additionalIngredientStyles = makeStyles(() =>
  createStyles({
    root: {
      width: 108,
      height: 200,
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
    ingredientName: {
      height: 32,
    },
  }),
);
