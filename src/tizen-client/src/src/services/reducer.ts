import { combineReducers } from "redux";
import { reducer as authReducer, AuthState } from "./auth/reducer";
import { reducer as moviesReducer, MoviesState } from "./movies/reducer";

export interface DataState {
  auth: AuthState;
  movies: MoviesState;
}

export const reducer = combineReducers({
  auth: authReducer,
  movies: moviesReducer,
});
