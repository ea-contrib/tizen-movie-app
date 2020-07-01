import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import Routes from "./Routes";
import { Sidebar, Footer } from "./components";
import { ReduxState } from "./Store";

import { initializeKeyboardActions } from "./utils/keyboardManagement";

import "spatial-navigation-polyfill/polyfill/spatial-navigation-polyfill";
import "./App.styl";

interface PathParams {}

interface Props extends RouteComponentProps<PathParams> {
  authenticated: boolean;
}

interface State {}

class App extends React.Component<Props, State> {
  componentDidMount(): void {
    initializeKeyboardActions(this.props.history);
  }

  render() {
    return (
      <>
        <div className="sidebar__wrapper">
          <Sidebar authenticated={this.props.authenticated} />
        </div>
        <div className="content__wrapper">
          <div className="main__wrapper">
            <main className="main">
              <Routes />
            </main>
          </div>
          <div className="footer__wrapper">
            <Footer authenticated={this.props.authenticated} />
          </div>
        </div>
      </>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({
  authenticated: state.data.auth.isAuthenticated,
});

const mapDispatchToProps = {};

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));
