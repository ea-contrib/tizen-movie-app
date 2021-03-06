import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";

import { ReduxState } from "../../Store";

interface PathParams {}
interface Props extends RouteComponentProps<PathParams> {}
interface State {}

class ApplicationError extends React.Component<Props, State> {
  render() {
    return <div>Error</div>;
  }
}

const mapStateToProps = (state: ReduxState) => ({});

const mapDispatchToProps = {};

export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(ApplicationError)
);
