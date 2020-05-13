import ReactDOM from "react-dom";
import React from "react";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";

import { default as App } from "./src/App";
import { store } from "./src/Store";

ReactDOM.render(
  <BrowserRouter>
    <Provider store={store}>
      <App />
    </Provider>
  </BrowserRouter>,
  document.querySelector(".app")
);
