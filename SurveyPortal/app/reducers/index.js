import { combineReducers } from 'redux';
import { reducer as formReducer } from 'redux-form';
import surveyCompletion from './surveyCompletion';
import createSurveyFirst from './createSurveyFirst';
import users from './users';
import groups from './groups';
import notifications from './notification';
import auth from './authentication';
import surveyResults from './surveyResults';
import error from './error';
import surveyOverview from './surveyOverview';
import user from './userReducer';
import survey from './survey';

export default combineReducers({
  surveyCompletion,
  createSurveyFirst,
  users,
  groups,
  auth,
  surveyResults,
  surveyOverview,
  form: formReducer,
  error,
  user,
  survey,
  notifications,
});
