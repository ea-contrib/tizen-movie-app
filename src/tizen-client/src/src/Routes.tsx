import * as React from "react";
import { Route, Switch } from "react-router-dom";
import {
  About,
  Authorization,
  Profile,
  Player,
  Home,
  MovieDetails,
  NotFound,
} from "./screens";
import { GuestRoute, SecuredRoute } from "./components";

interface Props {}

interface State {}

class Routes extends React.Component<Props, State> {
  render() {
    return (
      <Switch>
        <Route path="/" exact={true}>
          <Home />
        </Route>
        <Route path="/about" exact={true} component={About} />
        <GuestRoute
          path="/authorization"
          exact={true}
          component={Authorization}
        />
        <SecuredRoute path="/movies/:id">
          <MovieDetails />
        </SecuredRoute>
        <SecuredRoute path="/profile" exact={true}>
          <Profile />
        </SecuredRoute>
        <SecuredRoute path="/play/:id">
          <Player />
        </SecuredRoute>
        <Route component={NotFound} />
      </Switch>
    );
  }
}
export default Routes;
