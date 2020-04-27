import * as React from "react";
import { Switch, Route } from "react-router-dom";

import { NavigationBar } from "./components"
import { About, Home } from "./scenes"

import "./app.styl";

type Props = {

};
type State = {

};

export class App extends React.Component<Props, State> {
    render() {
        return (
            <div className="container">
                <NavigationBar className="navigation-bar"/>
                <Switch>
                    <Route path="/about">
                        <About />
                    </Route>
                    <Route path="/">
                        <Home />
                    </Route>
                </Switch>
            </div>
        );
    };
}
