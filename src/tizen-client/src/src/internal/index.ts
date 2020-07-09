import { combineReducers } from "redux";
import { reducer as formsReducer, FormsState } from "./form/reducer";

export interface InternalState {
  forms: FormsState;
}

export const reducer = combineReducers({
  forms: formsReducer,
});


export * from "./form"
