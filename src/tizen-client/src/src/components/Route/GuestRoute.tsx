import * as React from "react";
import { Route, Redirect } from "react-router-dom";

interface Props {
  exact?: boolean;
  path: string;
  component: React.ComponentType<any>;
  authenticated: boolean;
}

const GuestRoute = ({ component: Component, ...otherProps }: Props) => {
  if (otherProps.authenticated === true) {
    return <Redirect to="/" />;
  }
  return (
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
};
export default GuestRoute;
