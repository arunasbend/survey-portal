import { SURVEY_DATA_RETRIEVED } from '../actions/constants';

export default (state = {}, action) => {
  switch (action.type) {
    case SURVEY_DATA_RETRIEVED:
      return {
        ...state,
        data: action.data,
      };
    default:
      return state;
  }
};
