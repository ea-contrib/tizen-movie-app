import * as React from "react";
import { Route, Switch } from "react-router-dom";
import { About, Authorization, Profile, Home, NotFound } from "./screens";
import { GuestRoute, SecuredRoute } from "./components";

interface Props {
  authenticated: boolean;
}

interface State {}

class Routes extends React.Component<Props, State> {
  render() {
    return (
      <Switch>
        <Route path="/" exact={true} component={Home} />
        <Route path="/about" exact={true} component={About} />
        <GuestRoute
          path="/authorization"
          exact={true}
          component={Authorization}
          authenticated={this.props.authenticated}
        />
        <SecuredRoute
          path="/profile"
          exact={true}
          component={Profile}
          authenticated={this.props.authenticated}
        />
        <Route component={NotFound} />
      </Switch>
    );
  }
}
export default Routes;
