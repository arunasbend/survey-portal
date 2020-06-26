import { ADD_QUESTIONS } from '../actions/constants';

const initalState = {
  data: {
    userGroups: '',
    name: '',
    date: '',
  },
};

export default (state = initalState, action) => {
  switch (action.type) {
    case ADD_QUESTIONS:
      return {
        ...state,
        data: action.data,
      };
    default:
      return state;
  }
};
