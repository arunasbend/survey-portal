import { ADD_GROUP, REMOVE_GROUP, RENAME_GROUP } from '../actions/constants';

export default (state = {}, action) => {
  switch (action.type) {
    case ADD_GROUP:
      return {
        ...state,
        groups: [...state.groups, {
          title: action.title,
          members: 0,
          surveys: 0,
          id: action.id,
        }],
      };
    case REMOVE_GROUP:
      return {
        ...state,
        groups: state.groups.filter(item => item.id !== action.id),
      };
    case RENAME_GROUP:
      return {
        ...state,
        groups: state.groups.map((item) => {
          if (item.id === action.id) return { ...item, title: action.title };
          return item;
        }),
      };
    default:
      return state;
  }
};
