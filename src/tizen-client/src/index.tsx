import ReactDOM from "react-dom";
import React from "react";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";

import { default as App } from "./src/App";
import { history } from "./src/History";
import { store } from "./src/Store";

import "./index.styl";

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  document.querySelector(".app")
);
