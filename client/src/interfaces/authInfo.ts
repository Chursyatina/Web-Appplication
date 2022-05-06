import { IUser } from 'src/interfaces/user';

export interface IAuthInfo {
  id: string,
  isAuth: boolean;
  user: IUser;
  role: string;
}

export interface IAuthInfoProps {
  authInfo: IAuthInfo;
}
