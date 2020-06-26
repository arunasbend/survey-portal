/* eslint-disable no-unused-vars */
import {
  ADD_USER,
  REMOVE_USER,
  AUTH_USER,
  SHOW_ERROR,
  HIDE_ERROR,
  UNAUTH_USER,
  SET_USER_VISIBILITY_FILTER,
  UPDATE_USER_DATA,
  REMOVE_USER_DATA,
  RETRIEVE_USER_DATA_SUCCESS,
  SHOW_NOTIFICATION,
  HIDE_NOTIFICATION,
  SHOW_FORGOT_PASSWORD_ERROR,
} from './constants';

import api from '../utils/api';
import { sendNotification } from './notification';

let userId = 3;

export const addUser = (name, group) => {
  userId += 1;
  return ({
    type: ADD_USER,
    id: userId,
    name,
    group,
  });
};

export const removeUser = id => ({
  type: REMOVE_USER,
  id,
});

export const loginUser = (username, password) => (dispatch) => {
  api
  .post('/api/user/login', { username, password })
  .then((response) => {
    if (response.ok) {
      localStorage.setItem('login', true);
      dispatch({
        type: AUTH_USER,
      });
      dispatch({
        type: HIDE_ERROR,
      });
      dispatch({
        type: UPDATE_USER_DATA,
        data: response.data,
      });
      dispatch(sendNotification({
        message: 'You have succesfully logged in!',
        kind: 'success',
        dismissAfter: 2000,
      }));
    } else {
      dispatch({
        type: SHOW_ERROR,
        error: response.errorMessage,
      });
    }
  });
};

export const getUserData = (dispatch) => {
  api
  .get('/api/user/get-user')
  .then((response) => {
    dispatch({
      type: UPDATE_USER_DATA,
      data: response.data,
    });
  });
};

export const registerUser = registerFormData => (dispatch) => {
  api
  .post('/api/user/register', registerFormData)
  .then((response) => {
    if (response.ok) {
      localStorage.setItem('login', true);
      dispatch({
        type: UPDATE_USER_DATA,
        data: response.data,
      });
      dispatch({
        type: AUTH_USER,
      });
    } else {
      dispatch({
        type: SHOW_ERROR,
        error: response.errorMessage,
      });
    }
  });
};


export const logoutUser = () => (dispatch) => {
  api
  .post('/api/user/logout')
  .then((response) => {
    if (response.status !== 204) return;
    localStorage.removeItem('login');
    dispatch({
      type: UNAUTH_USER,
    });
    dispatch({
      type: REMOVE_USER_DATA,
    });
  });
};

export const setVisibilityFilter = filter => ({
  type: SET_USER_VISIBILITY_FILTER,
  filter,
});

export const retrieveUserData = () => (dispatch) => {
  api
  .get('/api/user/get-user')
  .then((response) => {
    dispatch({
      type: UPDATE_USER_DATA,
      data: response.data,
    });
  });
};

export const submitUserData = (id, data) => (dispatch) => {
  api
  .put(`/api/user/${id}`, data)
  .then(() => {
    dispatch(sendNotification({
      message: 'User profile has been updated!',
      kind: 'success',
      dismissAfter: 5000,
    }));
  })
  .catch(() => {
    dispatch(sendNotification({
      message: 'There was an error',
      kind: 'danger',
      dismissAfter: 5000,
    }));
  });
};

export const changeUserPassword = (email, password, token, history) => (dispatch) => {
  api
  .post('/api/user/resetpassword', { email, password, token })
  .then((response) => {
    if (response.ok) {
      history.push('/');
    } else {
      dispatch({
        type: SHOW_FORGOT_PASSWORD_ERROR,
        error: response.errorMessage,
      });
    }
  });
};

export const sendEmail = email => (dispatch) => {
  api
  .post('/api/user/forgotpassword', { email });
  dispatch(sendNotification({
    message: 'Email has been sent successfully',
    kind: 'success',
    dismissAfter: 5000,
  }));
};
