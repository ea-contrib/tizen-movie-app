import * as React from "react";
import { Route, Redirect } from "react-router-dom";
import { connect } from "react-redux";

import { ReduxState } from "../../Store";

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

const mapStateToProps = (state: ReduxState) => ({
  authenticated: state.data.auth.isAuthenticated,
});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(SecuredRoute);
