import * as React from "react";
import { Link } from "react-router-dom";

interface Props {
  authenticated: boolean;
  className: string;
}
interface State {}

class NavigationBar extends React.Component<Props, State> {
  render() {
    return (
      <nav className={this.props.className + " container"}>
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/about">About</Link>
          </li>
          {!this.props.authenticated && (
            <li>
              <Link to="/authorization">Authorization</Link>
            </li>
          )}
          {this.props.authenticated && (
            <li>
              <Link to="/profile">Profile</Link>
            </li>
          )}
        </ul>
      </nav>
    );
  }
}

export default NavigationBar;
