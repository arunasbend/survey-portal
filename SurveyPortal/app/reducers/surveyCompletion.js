import { LOAD_SURVEY, SUBMIT_SURVEY_ANSWERS, SURVEY_RETRIEVED } from '../actions/constants';

export default (state = [], action) => {
  switch (action.type) {
    case LOAD_SURVEY: {
      return {
        ...state,
        loading: true,
      };
    }
    case SURVEY_RETRIEVED: {
      return {
        ...state,
        ...action.surveyData,
        loading: false,
      };
    }
    case SUBMIT_SURVEY_ANSWERS: {
      const questions = state.questions.map((question, i) => ({
        ...question,
        data: action.data[i],
      }));
      return {
        ...state,
        questions,
      };
    }
    case 'CLEAR_SURVEY_COMPLETION_STATE':
      return {
        ...state,
        questions: [],
        title: '',
        description: '',
        dueDate: '',
        surveyId: '',
      };
    default:
      return state;
  }
};
