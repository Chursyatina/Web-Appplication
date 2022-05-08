import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';

export const pizzaStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      position: 'relative',
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
    divBack: {
      backgroundColor: 'grey',
      position: 'absolute',
      left: 0,
      right: 0,
      bottom: 0,
      top: 0,
      opacity: 0.9,
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'center',
    },
    divBackAdmin: {
      backgroundColor: 'grey',
      position: 'absolute',
      left: 0,
      right: 0,
      bottom: 0,
      top: 0,
      opacity: 0.8,
      height: 200,
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'center',
      pointerEvents: 'none',
    },
    notAvialable: {
      alignContent: 'center',
      alignSelf: 'center',
      verticalAlign: 'middle',
    },
  }),
);
