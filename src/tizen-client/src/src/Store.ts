import {
  compose,
  createStore,
  combineReducers,
  applyMiddleware,
  Store,
  Reducer,
} from "redux";
import thunk from "redux-thunk";
// import logger from 'redux-logger';

import { reducer as dataReducer } from "./services/reducer";
// import { reducer as scenesReducer } from './scenes/reducer';

declare global {
  interface Window {
    __REDUX_DEVTOOLS_EXTENSION_COMPOSE__?: typeof compose;
  }
}

const reducers: Reducer = combineReducers({
  data: dataReducer,
  // scenes: scenesReducer
});

const wrapEnhancersWithDevTools =
  window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const store: Store = createStore(
  reducers,
  wrapEnhancersWithDevTools(applyMiddleware(thunk /*, logger*/))
);
