import { Reducer } from "redux";

import {
  Action as FormAction,
  Form,
  REGISTER_FORM,
  REGISTER_FORM_CHANGES,
  UNREGISTER_FORM,
} from "./types";

export interface FormsState {
  activeForm: Form;
}

const initialState: FormsState = {
  activeForm: {
    id: "form",
    fields: new Map(),
  },
};

export const reducer: Reducer<FormsState, FormAction> = (
  state = initialState,
  action: FormAction
) => {
  switch (action.type) {
    case REGISTER_FORM:
      return Object.assign({}, state, {
        activeForm: action.payload.form,
      });
    case REGISTER_FORM_CHANGES:
      const formAfterUpdates = state.activeForm;
      formAfterUpdates.fields.set(
        action.payload.fieldId,
        action.payload.newValue
      );
      return Object.assign({}, state, {
        activeForm: formAfterUpdates,
      });
    case UNREGISTER_FORM:
      return Object.assign({}, state, {
        activeForm: initialState,
      });
    default:
      return state;
  }
};
