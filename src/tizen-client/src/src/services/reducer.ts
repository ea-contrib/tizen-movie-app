import { combineReducers } from "redux";
import { reducer as authReducer, AuthState } from "./auth/reducer";

export interface DataState {
  auth: AuthState;
}

export const reducer = combineReducers({
  auth: authReducer,
});
