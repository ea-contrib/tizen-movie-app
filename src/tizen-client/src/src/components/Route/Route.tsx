import * as React from "react";
import { ReactNode } from "react";
import { Route as RouterRoute } from "react-router-dom";
import { connect } from "react-redux";

import { ReduxState } from "../../Store";

interface IProps {
  exact?: boolean;
  path?: string;
  children: ReactNode;
  authenticated?: boolean;
}

const Route = ({ children, ...otherProps }: IProps) => {
  return (
    <>
      {/* <header>User Header</header> */}
      <RouterRoute {...otherProps} children={children} />
      {/* <footer>User Footer</footer> */}
    </>
  );
};

const mapStateToProps = (state: ReduxState) => ({
  authenticated: state.data.auth.isAuthenticated,
});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(Route);
