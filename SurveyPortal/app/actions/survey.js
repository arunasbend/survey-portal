/* eslint-disable no-unused-vars */
import api from '../utils/api';
import { SURVEY_DATA_RETRIEVED, SURVEY_OVERVIEW_RETRIEVED, SURVEY_DELETED_SUCCESS, SURVEY_RESULTS_DATA_RETRIEVED, LOAD_SURVEY_RESULTS, SHOW_ERROR, HIDE_ERROR } from './constants';
import { sendNotification } from './notification';

export const loadSurveyResults = id => (dispatch) => {
  dispatch({
    type: LOAD_SURVEY_RESULTS,
  });
  api
  .get(`/api/survey-submission/results/${id}`)
  .then((response) => {
    if (response.ok) {
      dispatch({
        type: HIDE_ERROR,
      });
      dispatch({
        type: SURVEY_RESULTS_DATA_RETRIEVED,
        surveyResults: response.data,
      });
    } else {
      dispatch({
        type: SHOW_ERROR,
        error: response.errorMessage,
      });
    }
  });
};

export const loadSurvey = id => (dispatch) => {
  api
    .get(`/api/surveys/edit/${id}`)
    .then((response) => {
      if (response.ok) {
        dispatch({
          type: SURVEY_DATA_RETRIEVED,
          data: response.data,
        });
      }
    });
};

export const getSurveyOverview = () => (dispatch) => {
  api
    .get('/api/surveys/overview/')
    .then((response) => {
      if (response.ok) {
        dispatch({
          type: SURVEY_OVERVIEW_RETRIEVED,
          data: response.data,
        });
      }
    });
};

export const deleteSurvey = id => (dispatch) => {
  api
    .$delete(`/api/surveys/${id}`)
    .then((response) => {
      if (response.ok) {
        dispatch({
          type: SURVEY_DELETED_SUCCESS,
          id,
        });
      }
    });
};

export const updateSurvey = (id, survey, history) => (dispatch) => {
  api
    .put(`/api/surveys/${id}`, survey)
    .then(() => {
      dispatch(sendNotification({
        message: 'Survey modified successfully!',
        kind: 'success',
        dismissAfter: 8000,
      }));
      history.push('/survey-overview');
    });
};
