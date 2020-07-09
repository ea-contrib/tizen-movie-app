import * as React from "react";

import "./InputField.styl";

interface Props {
  id: string;
  type?: string;
  value: string;

  onChange: (fieldId:string, newValue: string) => void;
}
interface State {}

export class InputField extends React.Component<Props, State> {
  constructor(props: Props) {
    super(props);

    this.handleChange = this.handleChange.bind(this);
  }

  componentDidUpdate() {
    console.log("Did update: ", this.props.value);
  }

  handleChange(event: any) {
    console.log(event.target.value);
    this.props.onChange(this.props.id, event.target.value);
  }

  render() {
    return (
      <input
        type={this.props.type ? this.props.type : "text"}
        className="form__input-field"
        value={this.props.value}
        onChange={this.handleChange}
      />
    );
  }
}
