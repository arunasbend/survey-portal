import { SURVEY_OVERVIEW_RETRIEVED, SURVEY_DELETED_SUCCESS } from '../actions/constants';

export default (state = {}, action) => {
  switch (action.type) {
    case SURVEY_OVERVIEW_RETRIEVED:
      return {
        ...state,
        survey: {
          ...state.survey,
          header: action.data.header,
          surveyManager: action.data.surveyManager,
        },
      };
    case SURVEY_DELETED_SUCCESS:
      return {
        ...state,
        survey: {
          ...state.survey,
          surveyManager: state.survey.surveyManager.filter(item => item.id !== action.id),
        },
      };
    default:
      return state;
  }
};
