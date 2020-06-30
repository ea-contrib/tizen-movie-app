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
import {
  connectRouter,
  routerMiddleware as navigationMiddleware,
  RouterState,
} from "connected-react-router";
import { History } from "history";

import { history } from "./History";
import { reducer as dataReducer, DataState } from "./services/reducer";
// import { reducer as screensReducer } from './screens/reducer';

declare global {
  interface Window {
    __REDUX_DEVTOOLS_EXTENSION_COMPOSE__?: typeof compose;
  }
}

export interface ReduxState {
  data: DataState;
  router: RouterState;
}

function createRootReducer(history: History): Reducer {
  return combineReducers({
    data: dataReducer,
    // screens: screensReducer,

    router: connectRouter(history),
  });
}

const wrapEnhancersWithDevTools =
  window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

export const store: Store = createStore(
  createRootReducer(history),
  wrapEnhancersWithDevTools(
    applyMiddleware(thunk, navigationMiddleware(history) /*, logger*/)
  )
);
