import * as React from "react";
import { Switch } from "react-router-dom";
import {
  About,
  Authorization,
  Profile,
  Player,
  Home,
  MovieDetails,
  NotFound,
} from "./screens";
import { GuestRoute, SecuredRoute, Route } from "./components";

interface Props {}

interface State {}

class Routes extends React.Component<Props, State> {
  render() {
    return (
      <Switch>
        <Route path="/" exact={true}>
          <Home />
        </Route>
        <Route path="/about" exact={true}>
          <About/>
        </Route>
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
        <Route>
          <NotFound/>
        </Route>
      </Switch>
    );
  }
}
export default Routes;
