import { SHOW_ERROR, HIDE_ERROR, SHOW_FORGOT_PASSWORD_ERROR } from '../actions/constants';

export default function (state = null, action) {
  switch (action.type) {
    case SHOW_ERROR: {
      return { ...state, errorMessage: action.error };
    }
    case HIDE_ERROR: {
      const { errorMessage, ...rest } = state;
      return rest;
    }
    case SHOW_FORGOT_PASSWORD_ERROR: {
      return { ...state, errorMessageForgotPassword: action.error };
    }
    default:
      return state;
  }
}
