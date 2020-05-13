import { Dispatch } from "redux";

import { MoviesFilter } from "../../models";

import * as api from "./api";
import * as types from "./types";

export const fetchMovies = (filter: MoviesFilter) => (
  dispatch: Dispatch<types.FetchMoviesAction>
) => {
  dispatch<types.FetchMoviesAction>({
    type: types.FETCH_MOVIES_REQUEST,
  });

  api
    .fetchMovies(filter)
    .then((data) => {
      dispatch<types.FetchMoviesAction>({
        type: types.FETCH_MOVIES_SUCCESS,
        payload: data,
      });
    })
    .catch((error) => {
      dispatch<types.FetchMoviesAction>({
        type: types.FETCH_MOVIES_FAILURE,
        error,
      });
    });
};

export const getMovie = (id: string) => (
  dispatch: Dispatch<types.GetMovieAction>
) => {
  dispatch<types.GetMovieAction>({
    type: types.GET_MOVIE_REQUEST,
  });

  api
    .getMovie(id)
    .then((data) => {
      dispatch<types.GetMovieAction>({
        type: types.GET_MOVIE_SUCCESS,
        payload: data,
      });
    })
    .catch((error) => {
      dispatch<types.GetMovieAction>({
        type: types.GET_MOVIE_FAILURE,
        error,
      });
    });
};
