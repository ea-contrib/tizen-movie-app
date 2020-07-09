import * as React from "react";
import { connect } from "react-redux";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { History } from "history";

import { login } from "../../services/auth";
import {
  registerForm,
  registerChanges,
  unregisterForm,
  Form,
} from "../../internal";
import { ReduxState } from "../../Store";
import { InputField } from "../../components";

import "./Authorization.styl";

interface PathParams {}

interface Props extends RouteComponentProps<PathParams> {
  form: Form;

  performLogin: (history: History) => void;
  registerForm: (form: Form) => void;
  registerFormChanges: (fieldId: string, newValue: any) => void;
  unregisterForm: () => void;
}

interface State {
  form: Form;
}

class Authorization extends React.Component<Props, State> {
  constructor(props: Props) {
    super(props);
    this.state = {
      form: {
        id: "loginForm",
        fields: new Map([
          ["email", ""],
          ["password", ""],
        ]),
      },
    };

    this.props.registerForm(this.state.form);
    this.changeFieldValue = this.changeFieldValue.bind(this);
  }

  changeFieldValue(fieldId: string, newValue: any | null) {
    const formAfterUpdates = this.state.form;
    formAfterUpdates.fields.set(fieldId, newValue);
    this.setState({
      form: formAfterUpdates,
    });
  }

  componentWillUnmount() {
    this.props.unregisterForm();
  }

  render() {
    return (
      <div className="login__wrapper">
        <div className="login__form">
          <h1>Sign In</h1>
          <div className="form__wrapper">
            <div className="form__field">
              <label>Email</label>
              <InputField
                id="email"
                type="email"
                value={this.state.form.fields.get("email")}
                onChange={this.changeFieldValue}
              />
            </div>
            <div className="form__field">
              <label htmlFor="login-form__password">Password</label>
              <input
                id="login-form__password"
                type="passwod"
                className="form__field-input"
              />
            </div>
            <button onClick={() => this.props.performLogin(this.props.history)}>
              Sign In
            </button>
          </div>
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state: ReduxState) => ({
  form: state.internal.forms.activeForm,
});

const mapDispatchToProps = {
  performLogin: login,

  registerForm: registerForm,
  registerFormChanges: registerChanges,
  unregisterForm: unregisterForm,
};

export default withRouter(
  connect(mapStateToProps, mapDispatchToProps)(Authorization)
);
