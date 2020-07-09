export interface Form {
  id: string;
  fields: Map<string, any | null>;
}

export const REGISTER_FORM = "internal/forms/register";
export interface RegisterForm {
  type: typeof REGISTER_FORM;
  payload: {
    form: Form;
  };
}
export type RegisterFormAction = RegisterForm;

export const REGISTER_FORM_CHANGES = "internal/forms/change";
export interface RegisterFromChanges {
  type: typeof REGISTER_FORM_CHANGES;
  payload: {
    fieldId: string;
    newValue: any | null;
  };
}
export type RegisterChangesAction = RegisterFromChanges;

export const UNREGISTER_FORM = "internal/forms/unregister";
export interface UnregisterForm {
  type: typeof UNREGISTER_FORM;
}
export type UnregisterFormAction = UnregisterForm;

export type Action =
  | RegisterFormAction
  | RegisterChangesAction
  | UnregisterFormAction;
