import * as React from "react";
import { connect } from "react-redux";

import { login } from "../../services/auth";

type Props = {
  performLogin: () => void;
};

type State = {};

class Authorization extends React.Component<Props, State> {
  render() {
    return (
      <>
        <div>Authorization</div>
        <button onClick={this.props.performLogin}>Login</button>
      </>
    );
  }
}

const mapDispatchToProps = {
  performLogin: login,
};

export default connect(null, mapDispatchToProps)(Authorization);
