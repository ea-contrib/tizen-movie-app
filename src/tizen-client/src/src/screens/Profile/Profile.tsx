import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { History } from "history";

import { logout } from "../../services/auth";
import { ReduxState } from "../../Store";

interface PathParams {}

interface Props extends RouteComponentProps<PathParams> {
  performLogout: (history: History) => void;
}

interface State {}

class Profile extends React.Component<Props, State> {
  render() {
    return (
      <>
        <div>Profile</div>
        <button onClick={() => this.props.performLogout(this.props.history)}>
          Logout
        </button>
      </>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({});

const mapDispatchToProps = {
  performLogout: logout,
};

export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(Profile)
);
