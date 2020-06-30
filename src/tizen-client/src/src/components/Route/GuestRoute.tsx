import * as React from "react";
import { Route, Redirect } from "react-router-dom";
import { connect } from "react-redux";

import { ReduxState } from "../../Store";

import { tempFocusOnTopLink } from "../../utils/keyboardNavigation";

interface Props {
  exact?: boolean;
  path: string;
  component: React.ComponentType<any>;
  authenticated: boolean;
}

const GuestRoute = ({ component: Component, ...otherProps }: Props) => {
  tempFocusOnTopLink();
  if (otherProps.authenticated === true) {
    return <Redirect to="/" />;
  }
  return (
    <>
      {/* <header>Guest Header</header> */}
      <Route
        render={(otherProps) => (
          <>
            <Component {...otherProps} />
          </>
        )}
      />
      {/* <footer>Guest Footer</footer> */}
    </>
  );
};

const mapStateToProps = (state: ReduxState) => ({
  authenticated: state.data.auth.isAuthenticated,
});

const mapDispatchToProps = {};

export default connect(mapStateToProps, mapDispatchToProps)(GuestRoute);
