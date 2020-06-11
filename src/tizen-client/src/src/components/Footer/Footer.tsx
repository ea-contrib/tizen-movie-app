import * as React from "react";
import { Link } from "react-router-dom";

interface Props {
  authenticated: boolean;
}
interface State {}

export class Footer extends React.Component<Props, State> {
  render() {
    return <footer>test</footer>;
  }
}
