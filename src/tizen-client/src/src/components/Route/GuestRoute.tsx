import * as React from "react";
import { Route } from "react-router-dom";

interface IProps {
  exact?: boolean;
  path: string;
  component: React.ComponentType<any>;
}

const GuestRoute = ({ component: Component, ...otherProps }: IProps) => (
  <>
    <header>Guest Header</header>
    <Route
      render={(otherProps) => (
        <>
          <Component {...otherProps} />
        </>
      )}
    />
    <footer>Guest Footer</footer>
  </>
);
export default GuestRoute;
