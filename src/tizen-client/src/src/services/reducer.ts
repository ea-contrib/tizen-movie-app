import { combineReducers } from 'redux';
import { reducer as authReducer } from './auth/reducer';

export const reducer = combineReducers({
    auth: authReducer
});