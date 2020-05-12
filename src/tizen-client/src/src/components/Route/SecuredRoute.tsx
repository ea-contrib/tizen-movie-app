import * as React from "react";
import { Route } from "react-router-dom";

interface IProps {
  exact?: boolean;
  path: string;
  component: React.ComponentType<any>;
}

const SecuredRoute = ({ component: Component, ...otherProps }: IProps) => (
  <>
    <header>User Header</header>
    <Route
      render={(otherProps) => (
        <>
          <Component {...otherProps} />
        </>
      )}
    />
    <footer>User Footer</footer>
  </>
);
export default SecuredRoute;
