import { Dispatch } from "redux";

import * as api from "./api";
import * as types from "./types";

export const login = () => (dispatch: Dispatch<types.LoginAction>) => {
  dispatch<types.LoginAction>({
    type: types.LOGIN_REQUEST,
  });

  api
    .performLogin()
    .then((data) => {
      dispatch<types.LoginAction>({
        type: types.LOGIN_SUCCESS,
        payload: data,
      });
    })
    .catch((error) => {
      dispatch<types.LoginAction>({
        type: types.LOGIN_FAILURE,
        error,
      });
    });
};

export const logout = () => (dispatch: Dispatch<types.LogoutAction>) => {
  dispatch<types.LogoutAction>({
    type: types.LOGOUT_REQUEST,
  });

  api
    .performLogout()
    .then((data) => {
      dispatch<types.LogoutAction>({
        type: types.LOGOUT_SUCCESS,
      });
    })
    .catch((error) => {
      dispatch<types.LogoutAction>({
        type: types.LOGOUT_FAILURE,
        error,
      });
    });
};
