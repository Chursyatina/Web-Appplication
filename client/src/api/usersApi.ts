import { USERS_URL } from 'src/consts/urls';
import { IAuthInfo } from 'src/interfaces/authInfo';
import { ISignInForm } from 'src/interfaces/DTOs/SignInForm';
import { ISignUpForm } from 'src/interfaces/DTOs/SignUpForm';

export const getCurrentuser = async (): Promise<IAuthInfo> => {
  const response = await fetch(`${USERS_URL}/authinfo`);
  return (await response.json()) as IAuthInfo;
};

export const signOut = async () => {
  const response = await fetch(USERS_URL + "/signout", {
      method: "POST",
      headers: {
          "Content-Type": "application/json",
      },
  });
  return response;
};

export const signUp = async (signupForm: ISignUpForm) => {
  const response = await fetch(USERS_URL + "/signup", {
      method: "POST",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(signupForm),
  });
  return response;
};

export const signIn = async (signinForm: ISignInForm) => {
  const response = await fetch(USERS_URL + "/signin", {
      method: "POST",
      headers: {
          "Content-Type": "application/json",
      },
      body: JSON.stringify(signinForm),
  });
  return response;
};