import { createStyles, makeStyles } from '@material-ui/core/styles';

export const pizzaListStyles = makeStyles(() =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    summary: {
      width: '80%',
      height: '100%',
      padding: 10,
    },
    button: {
      marginTop: 7,
      padding: 7,
    },
    cardMargin: {
      margin: 10,
    },
    actionArea: {
      padding: 30,
    },
    expander: {
      textAlign: 'right',
    },
  }),
);
