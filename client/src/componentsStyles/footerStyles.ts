import { createStyles, makeStyles } from '@material-ui/core/styles';

export const footerStyles = makeStyles(() =>
  createStyles({
    root: {
      background: '#f2f2f2',
      opacity: '0.7',
      fontSize: '14px',
      height: '120px',
      marginTop: '-120px',
    },
    content: {
      padding: '0 25px',
    },
  }),
);
