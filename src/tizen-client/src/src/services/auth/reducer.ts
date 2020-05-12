import { Reducer } from "redux";

import { Action, LOGIN_SUCCESS, LOGOUT_SUCCESS } from "./types";
import { User } from "../../models";

export interface State {
  isAuthenticated: boolean;
  loggedUser: User | null;
}

const initialState: State = {
  isAuthenticated: false,
  loggedUser: null,
};

export const reducer: Reducer<State> = (
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
