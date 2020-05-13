import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { History } from "history";

import { login } from "../../services/auth";
import { ReduxState } from "../../Store";

interface PathParams {}

interface Props extends RouteComponentProps<PathParams> {
  performLogin: (history: History) => void;
}

interface State {}

class Authorization extends React.Component<Props, State> {
  render() {
    return (
      <>
        <div>Authorization</div>
        <button onClick={() => this.props.performLogin(this.props.history)}>
          Login
        </button>
      </>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({});

const mapDispatchToProps = {
  performLogin: login,
};

export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(Authorization)
);
