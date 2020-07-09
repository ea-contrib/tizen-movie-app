import { Dispatch } from "redux";

import * as types from "./types";

export const registerForm = (form: types.Form) => (
  dispatch: Dispatch<types.RegisterFormAction>
) => {
  dispatch<types.RegisterFormAction>({
    type: types.REGISTER_FORM,
    payload: {
      form: form,
    },
  });
};

export const registerChanges = (fieldId: string, newValue: any | null) => (
  dispatch: Dispatch<types.RegisterChangesAction>
) => {
  dispatch<types.RegisterChangesAction>({
    type: types.REGISTER_FORM_CHANGES,
    payload: {
      fieldId: fieldId,
      newValue: newValue,
    },
  });
};

export const unregisterForm = () => (
  dispatch: Dispatch<types.UnregisterFormAction>
) => {
  dispatch<types.UnregisterFormAction>({
    type: types.UNREGISTER_FORM,
  });
};
