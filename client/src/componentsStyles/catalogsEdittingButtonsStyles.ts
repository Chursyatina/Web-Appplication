import { createStyles, makeStyles } from '@material-ui/core/styles';

export const catalogsEdittingButtonsStyles = makeStyles(() =>
  createStyles({
    root: {
      maxWidth: 370,
      margin: 15,
    },
    button: {
      margin: 2,
    },
    center: {
      textAlign: 'center',
    },
  }),
);
