import { createStyles, makeStyles } from '@material-ui/core/styles';

export const ingredientListStyles = makeStyles(() =>
  createStyles({
    withNoMarginsAndPaddings: {
      padding: 0,
      margin: 0,
    },
    crossedText: {
      textDecoration: 'line-through',
    },
    emptyStyle: {},
  }),
);
