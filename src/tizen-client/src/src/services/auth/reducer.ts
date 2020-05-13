import { Reducer } from "redux";

import { Action, LOGIN_SUCCESS, LOGOUT_SUCCESS } from "./types";
import { User } from "../../models";

export interface AuthState {
  isAuthenticated: boolean;
  loggedUser: User | null;
}

const initialState: AuthState = {
  isAuthenticated: false,
  loggedUser: null,
};

export const reducer: Reducer<AuthState> = (
  state = initialState,
  action: Action
) => {
  switch (action.type) {
    case LOGIN_SUCCESS:
      return {
        isAuthenticated: true,
        loggedUser: action.payload,
      };
    case LOGOUT_SUCCESS:
      return {
        isAuthenticated: false,
        loggedUser: null,
      };
    default:
      return state;
  }
};
