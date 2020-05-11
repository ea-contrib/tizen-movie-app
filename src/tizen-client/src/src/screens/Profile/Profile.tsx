import * as React from "react";
import { connect } from "react-redux";

import { logout } from '../../services/auth'

type Props = {
	performLogout: () => void
};

type State = {

};

class Profile extends React.Component<Props, State> {

	render() {
		return (
			<>
				<div>Profile</div>
				<button onClick={this.props.performLogout}>Logout</button>
			</>
		);
	}
}

const mapDispatchToProps = {
	performLogout: logout
};

export default connect(
	null,
	mapDispatchToProps,
)(Profile);