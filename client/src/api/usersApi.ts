import { USERS_URL } from 'src/consts/urls';
import { IAuthInfo } from 'src/interfaces/authInfo';

export const getCurrentuser = async (): Promise<IAuthInfo> => {
  const response = await fetch(`${USERS_URL}/authinfo`);
  return response.json();
};
