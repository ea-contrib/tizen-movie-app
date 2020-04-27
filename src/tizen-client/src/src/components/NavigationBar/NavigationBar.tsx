import * as React from "react";
import { Link } from "react-router-dom";

type Props = {
	className: string
};
type State = {

};

class NavigationBar extends React.Component<Props, State> {

	render() {
		return (
		<nav className={this.props.className + ' container'}>
			<ul>
				<li>
					<Link to="/">Home</Link>
				</li>
				<li>
					<Link to="/about">About</Link>
				</li>
			</ul>
		</nav>
		);
	}
}

export default NavigationBar;