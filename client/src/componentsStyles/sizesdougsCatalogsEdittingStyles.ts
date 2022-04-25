import { createStyles, makeStyles } from '@material-ui/core/styles';

export const sizesDoughsCatalogsEdittingStyles = makeStyles(theme =>
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
    iconcenter: {
      '& .MuiIconButton-root': {
        padding: 0,
      },
      textAlign: 'center',
      padding: 6,
    },
  }),
);
