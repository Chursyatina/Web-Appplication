/* eslint-disable */

export interface ISignInForm {
    Phone: string;
    Password: string;
    RememberMe: boolean;
    ReturnUrl: string;
}
  
  export interface ISignInFormProps {
    signInForm: ISignInForm;
  }
  