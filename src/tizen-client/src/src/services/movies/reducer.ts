import { combineReducers, Reducer } from "redux";

import {
  Action,
  FetchMoviesAction,
  GetMovieAction,
  FETCH_MOVIES_SUCCESS,
  GET_MOVIE_SUCCESS,
} from "./types";
import { Movie, MoviesFilter } from "../../models";

interface AllFetchedState {
  filter: MoviesFilter;
  movies: Array<Movie>;
}

const initialListState: AllFetchedState = {
  filter: {},
  movies: [],
};

interface MovieDetailsState {
  openedMovie: Movie | null;
}

const initialDetailsState: MovieDetailsState = {
  openedMovie: null,
};

export interface MoviesState {
  allFetched: AllFetchedState;
  movieDetails: MovieDetailsState;
}

const allFetchedReducer: Reducer<AllFetchedState, FetchMoviesAction> = (
  state = initialListState,
  action: FetchMoviesAction
) => {
  switch (action.type) {
    case FETCH_MOVIES_SUCCESS:
      const newState = {
        filter: state.filter,
        movies: action.payload,
      };
      return newState;
    default:
      return state;
  }
};

const movieDetailsReducer: Reducer<MovieDetailsState, GetMovieAction> = (
  state = initialDetailsState,
  action: GetMovieAction
) => {
  switch (action.type) {
    case GET_MOVIE_SUCCESS:
      const newState = {
        openedMovie: action.payload,
      };
      return newState;
    default:
      return state;
  }
};

export const reducer: Reducer<MoviesState, Action> = combineReducers({
  allFetched: allFetchedReducer,
  movieDetails: movieDetailsReducer,
});
