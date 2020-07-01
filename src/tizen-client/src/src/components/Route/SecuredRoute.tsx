import * as React from "react";
import { ReactNode } from "react";
import { Route, Redirect } from "react-router-dom";
import { connect } from "react-redux";

import { ReduxState } from "../../Store";

interface IProps {
  exact?: boolean;
  path: string;
  children: ReactNode;
  authenticated: boolean;
}

const SecuredRoute = ({ children, ...otherProps }: IProps) => {
  if (!otherProps.authenticated) {
    return <Redirect to="/authorization" />;
  }
  return (
    <>
      {/* <header>User Header</header> */}
      <Route {...otherProps} children={children} />
      {/* <footer>User Footer</footer> */}
    </>
  );
};

const mapStateToProps = (state: ReduxState) => ({
  authenticated: state.data.auth.isAuthenticated,
});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(SecuredRoute);
