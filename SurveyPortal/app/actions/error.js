import { SHOW_ERROR, HIDE_ERROR } from '../actions/constants';

export const showError = error => ({
  type: SHOW_ERROR,
  error,
});

export const hideError = () => ({
  type: HIDE_ERROR,
});
