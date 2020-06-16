import * as React from "react";
import { Link } from "react-router-dom";

interface Props {
  url: string;
}
interface State {}

export class MoviePoster extends React.Component<Props, State> {
  render() {
    return <img src={this.props.url} />;
  }
}
