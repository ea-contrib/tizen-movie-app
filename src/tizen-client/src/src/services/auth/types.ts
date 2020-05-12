import { User } from '../../models';

export const LOGIN_REQUEST = 'services/auth/login/request';
export interface LoginRequest {
    type: typeof LOGIN_REQUEST;
};
export const LOGIN_SUCCESS = 'services/auth/login/success';
export interface LoginSuccess {
    type: typeof LOGIN_SUCCESS;
    payload: User;
}
export const LOGIN_FAILURE = 'services/auth/login/failure';
export interface LoginFailure {
    type: typeof LOGIN_FAILURE;
    error: {};
};
export type LoginAction = 
    | LoginRequest
    | LoginSuccess
    | LoginFailure;

export const LOGOUT_REQUEST = 'services/auth/logout/request';
export type LogoutRequest = {
    type: typeof LOGOUT_REQUEST;
};
export const LOGOUT_SUCCESS = 'services/auth/logout/success';
export interface LogoutSuccess { 
    type: typeof LOGOUT_SUCCESS;
};
export const LOGOUT_FAILURE = 'services/auth/logout/failure';
export interface LogoutFailure { 
    type: typeof LOGOUT_FAILURE;
    error: {};
};
export type LogoutAction = 
    | LogoutRequest
    | LogoutSuccess
    | LogoutFailure;

export type Action =
    | LoginAction
    | LogoutAction;