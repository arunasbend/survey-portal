import {
  RETRIEVE_USER_DATA_SUCCESS,
  USER_SIGNUP,
  UPDATE_USER_DATA,
  REMOVE_USER_DATA,
} from '../actions/constants';

export default (state = {}, action) => {
  switch (action.type) {
    case USER_SIGNUP:
      return {
        ...state,
        userData: action.userData,
      };
    case RETRIEVE_USER_DATA_SUCCESS: {
      return {
        ...state,
        userData: action.userData,
      };
    }
    case UPDATE_USER_DATA:
      return {
        ...state,
        userData: {
          username: action.data.username,
          firstname: action.data.firstname,
          lastname: action.data.lastname,
          email: action.data.email,
          id: action.data.id,
        },
      };
    case REMOVE_USER_DATA:
      return {
        ...state,
        userData: {
          username: '',
          firstname: '',
          lastname: '',
          email: '',
          id: '',
        },
      };
    default:
      return state;
  }
};
