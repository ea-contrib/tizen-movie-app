import * as React from "react";
import { Route, Redirect } from "react-router-dom";

interface IProps {
  exact?: boolean;
  path: string;
  component: React.ComponentType<any>;
  authenticated: boolean;
}

const SecuredRoute = ({ component: Component, ...otherProps }: IProps) => {
  if (otherProps.authenticated === false) {
    return <Redirect to="/authorization" />;
  }
  return (
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
};
export default SecuredRoute;
