import { ADD_USER, REMOVE_USER, SET_USER_VISIBILITY_FILTER } from '../actions/constants';

export default (state = {}, action) => {
  switch (action.type) {
    case ADD_USER:
      return {
        ...state,
        users: [...state.users, {
          name: action.name,
          group: action.group,
          id: action.id,
        }],
      };
    case REMOVE_USER:
      return {
        ...state,
        users: state.users.filter(item => item.id !== action.id),
      };
    case SET_USER_VISIBILITY_FILTER:
      return {
        ...state,
        visibilityFilter: action.filter,
      };
    default:
      return state;
  }
};
