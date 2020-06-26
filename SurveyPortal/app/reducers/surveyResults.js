import { LOAD_SURVEY_RESULTS, SURVEY_RESULTS_DATA_RETRIEVED } from '../actions/constants';

export default (state = {}, action) => {
  switch (action.type) {
    case LOAD_SURVEY_RESULTS:
      return {
        ...state,
        loading: true,
      };
    case SURVEY_RESULTS_DATA_RETRIEVED:
      return {
        ...state,
        ...action.surveyResults,
        loading: false,
      };
    default:
      return state;
  }
};
