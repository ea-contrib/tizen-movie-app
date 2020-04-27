import ReactDOM from "react-dom";
import React from "react";
import { BrowserRouter } from "react-router-dom";

import {App} from "./src/App";

ReactDOM.render(
    <BrowserRouter>
        <App />
    </BrowserRouter>, 
    document.querySelector(".app")
);
