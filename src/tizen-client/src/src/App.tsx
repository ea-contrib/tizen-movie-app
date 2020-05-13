import * as React from "react";
import { connect } from "react-redux";

import Routes from "./Routes";
import { NavigationBar } from "./components";
import { ReduxState } from "./Store";

import "./app.styl";

interface Props {
  authenticated: boolean;
}

interface State {}

class App extends React.Component<Props, State> {
  render() {
    return (
      <div className="container">
        <NavigationBar
          authenticated={this.props.authenticated}
          className="navigation-bar"
        />
        <Routes authenticated={this.props.authenticated} />
      </div>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({
  authenticated: state.data.auth.isAuthenticated,
});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(App);
