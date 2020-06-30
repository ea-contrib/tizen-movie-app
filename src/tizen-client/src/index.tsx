import ReactDOM from "react-dom";
import React from "react";
import { Provider } from "react-redux";
import { MemoryRouter } from "react-router-dom";

import { default as App } from "./src/App";
import { store } from "./src/Store";

import "regenerator-runtime/runtime.js";
import "./index.styl"

ReactDOM.render(
  <MemoryRouter>
    <Provider store={store}>
      <App />
    </Provider>
  </MemoryRouter>,
  document.querySelector(".app")
);
