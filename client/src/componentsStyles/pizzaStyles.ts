import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

export const pizzaStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: 370,
      margin: 15,
    },
    media: {
      height: 200,
    },
    pizzaDescription: {
      height: 50,
    },
    button: {
      margin: 2,
      fontFamily: 'Segoe UI',
    },
    expand: {
      transform: 'rotate(0deg)',
      marginLeft: 'auto',
      transition: theme.transitions.create('transform', {
        duration: theme.transitions.duration.shortest,
      }),
    },
    expandOpen: {
      transform: 'rotate(180deg)',
    },
    cardActions: {
      padding: 15,
    },
    withNoMarginsAndPaddings: {
      padding: 0,
      margin: 0,
    },
    cardContent: {
      padding: 16,
    },
  }),
);
