import { createStyles, makeStyles } from '@material-ui/core/styles';

export const sizeTabsStyles = makeStyles(() =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    tab: {
      minWidth: 100,
      width: 100,
    },
  }),
);
