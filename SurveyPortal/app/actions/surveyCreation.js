/* eslint-disable no-unused-vars */
import api from '../utils/api';
import { sendNotification } from './notification';

export const createSurvey = (survey, history) => (dispatch) => {
  api
    .post('api/surveys', survey)
    .then(() => {
      history.push('/');
      dispatch(sendNotification({
        message: 'Survey created successfully!',
        kind: 'success',
        dismissAfter: 8000,
      }));
    });
};

export const showCreateSurveyError = () => (dispatch) => {
  dispatch(sendNotification({
    message: 'Please add questions!',
    kind: 'danger',
    dismissAfter: 8000,
  }));
};
