import * as React from "react";
import { Switch, Route } from "react-router-dom";

import Routes from './Routes'
import { NavigationBar } from "./components"

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
                <Routes />
            </div>
        );
    };
}
