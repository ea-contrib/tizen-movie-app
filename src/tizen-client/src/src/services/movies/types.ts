import { Movie } from "../../models";

export const FETCH_MOVIES_REQUEST = "services/movies/fetch/request";
export interface FetchMoviesRequest {
  type: typeof FETCH_MOVIES_REQUEST;
}
export const FETCH_MOVIES_SUCCESS = "services/movies/fetch/success";
export interface FetchMoviesSuccess {
  type: typeof FETCH_MOVIES_SUCCESS;
  payload: Array<Movie>;
}
export const FETCH_MOVIES_FAILURE = "services/movies/fetch/failure";
export interface FetchMoviesFailure {
  type: typeof FETCH_MOVIES_FAILURE;
  error: {};
}
export type FetchMoviesAction =
  | FetchMoviesRequest
  | FetchMoviesSuccess
  | FetchMoviesFailure;

export const GET_MOVIE_REQUEST = "services/movies/get/request";
export type GetMovieRequest = {
  type: typeof GET_MOVIE_REQUEST;
};
export const GET_MOVIE_SUCCESS = "services/movies/get/success";
export interface GetMovieSuccess {
  type: typeof GET_MOVIE_SUCCESS;
  payload: Movie;
}
export const GET_MOVIE_FAILURE = "services/movies/get/failure";
export interface GetMovieFailure {
  type: typeof GET_MOVIE_FAILURE;
  error: {};
}
export type GetMovieAction =
  | GetMovieRequest
  | GetMovieSuccess
  | GetMovieFailure;

export type Action = FetchMoviesAction | GetMovieAction;
