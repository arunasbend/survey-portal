/* eslint-disable no-unused-vars */
import api from '../utils/api';
import { SHOW_NOTIFICATION, HIDE_NOTIFICATION, LOAD_SURVEY, SURVEY_RETRIEVED } from './constants';
import { sendNotification } from './notification';

export const loadSurveyById = surveyId => (dispatch) => {
  api
  .get(`/api/surveys/${surveyId}`)
  .then((response) => {
    if (!response.ok) return;
    dispatch({
      type: SURVEY_RETRIEVED,
      surveyData: response.data,
    });
  });
};

export const submitSurveyQuestions = (surveySubmission, history) => (dispatch) => {
  api
    .post('/api/survey-submission', surveySubmission)
    .then(() => {
      dispatch(sendNotification({
        message: 'Survey completed!',
        kind: 'success',
        dismissAfter: 5000,
      }));
      history.push('/survey-overview');
      dispatch({
        type: 'CLEAR_SURVEY_COMPLETION_STATE',
      });
    });
};
